using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    PlayerController pc;
    private Rigidbody rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); //중력 적용 컴포넌트 가져옴
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public bool Run(Vector3 targetPos)
    {
        // 이동하고자하는 좌표 값과 현재 내 위치의 차이를 구한다.
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance >= 0.02f) // 차이가 아직 있다면
        {
            // 캐릭터를 이동시킨다.
            transform.localPosition = Vector3.MoveTowards(transform.position, targetPos, pc.moveSpeed * Time.deltaTime);
            pc.anim.Play("Witch_Walk"); // 걷기 애니메이션 처리
            return true;
        }
        return false;
    }
 
    public void Turn(Vector3 targetPos)
    {
        // 캐릭터를 이동하고자 하는 좌표값 방향으로 회전시킨다
        Vector3 dir = targetPos - transform.position;   //방향값을 계산하여 dir에 넣어둠
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);  //위에서 계산한 방향값에서 회전을 담당하는 x와 z값만 가져옴
 
        //Quaternion.LookRotation(벡터값), target을 기준으로 회전한다.
        Quaternion targetRot = Quaternion.LookRotation(dirXZ);
 
        //Rigibody의 강체를 . rotation 으로 변환하면 다음 물리 시뮬레이션 단계 후에 변환이 업데이트 된다. 
        rigid.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 1000.0f * Time.deltaTime);
    }
}
