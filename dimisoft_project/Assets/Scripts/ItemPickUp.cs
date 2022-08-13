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

    // �÷��̾ ������ �ֺ� ���� �Ÿ� �̻� ������ �������� �ڵ����� �÷��̾�� �̵�

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
        // Player�� ���� ��ġ�� �޾ƿ��� Object
        target = GameObject.Find("Player").transform;
        // Player�� ��ġ�� �� ��ü�� ��ġ�� ���� ���� ����ȭ �Ѵ�.
        direction = (target.position - transform.position).normalized;
        // ���ӵ� ���� (���� ���� ����, �Ÿ� �� ����ؼ� ������ ��)
        accelaration = 0.2f;
        // �ʰ� �ƴ� �� ���������� ���ӵ� ����Ͽ� �ӵ� ����
        velocity = (velocity + accelaration * Time.deltaTime);
        // Player�� ��ü ���� �Ÿ� ���
        float distance = Vector3.Distance(target.position, transform.position);
        // �����Ÿ� �ȿ� ���� ��, �ش� �������� ����
        if (distance <= 3.0f)
        {
            this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
                                                   transform.position.y + (direction.y * velocity),
                                                     transform.position.z + (direction.z * velocity));
        }
        // �����Ÿ� �ۿ� ���� ��, �ӵ� �ʱ�ȭ 
        else
        {
            velocity = 0.0f;
        }
    }

}