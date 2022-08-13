using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    PlayerController pc;
    private Rigidbody rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); //�߷� ���� ������Ʈ ������
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public bool Run(Vector3 targetPos)
    {
        // �̵��ϰ����ϴ� ��ǥ ���� ���� �� ��ġ�� ���̸� ���Ѵ�.
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance >= 0.02f) // ���̰� ���� �ִٸ�
        {
            // ĳ���͸� �̵���Ų��.
            transform.localPosition = Vector3.MoveTowards(transform.position, targetPos, pc.moveSpeed * Time.deltaTime);
            pc.anim.Play("Witch_Walk"); // �ȱ� �ִϸ��̼� ó��
            return true;
        }
        return false;
    }
 
    public void Turn(Vector3 targetPos)
    {
        // ĳ���͸� �̵��ϰ��� �ϴ� ��ǥ�� �������� ȸ����Ų��
        Vector3 dir = targetPos - transform.position;   //���Ⱚ�� ����Ͽ� dir�� �־��
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);  //������ ����� ���Ⱚ���� ȸ���� ����ϴ� x�� z���� ������
 
        //Quaternion.LookRotation(���Ͱ�), target�� �������� ȸ���Ѵ�.
        Quaternion targetRot = Quaternion.LookRotation(dirXZ);
 
        //Rigibody�� ��ü�� . rotation ���� ��ȯ�ϸ� ���� ���� �ùķ��̼� �ܰ� �Ŀ� ��ȯ�� ������Ʈ �ȴ�. 
        rigid.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 1000.0f * Time.deltaTime);
    }
}
