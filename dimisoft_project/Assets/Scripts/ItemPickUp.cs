using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    public Item GetItem()
    {
        return item;
    }
    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    // 플레이어가 아이템 주변 일정 거리 이상 들어오면 아이템이 자동으로 플레이어에게 이동

    Transform target;
    Vector3 direction;
    float velocity;
    float accelaration;

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    public void MoveToTarget()
    {
        // Player의 현재 위치를 받아오는 Object
        target = GameObject.Find("Player").transform;
        // Player의 위치와 이 객체의 위치를 빼고 단위 벡터화 한다.
        direction = (target.position - transform.position).normalized;
        // 가속도 지정 (추후 힘과 질량, 거리 등 계산해서 수정할 것)
        accelaration = 0.2f;
        // 초가 아닌 한 프레임으로 가속도 계산하여 속도 증가
        velocity = (velocity + accelaration * Time.deltaTime);
        // Player와 객체 간의 거리 계산
        float distance = Vector3.Distance(target.position, transform.position);
        // 일정거리 안에 있을 시, 해당 방향으로 무빙
        if (distance <= 3.0f)
        {
            this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
                                                   transform.position.y + (direction.y * velocity),
                                                     transform.position.z + (direction.z * velocity));
        }
        // 일정거리 밖에 있을 시, 속도 초기화 
        else
        {
            velocity = 0.0f;
        }
    }

}