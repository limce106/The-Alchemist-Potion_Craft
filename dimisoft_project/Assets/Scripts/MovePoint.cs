using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    // �� �̵� �� ������ ������ �̸�
    public string doorPoint;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        if (doorPoint == PlayerDataManager.instance.movePoint)
        {
            // �÷��̾��� ��ġ�� movePoint�� ����
            player.transform.position = this.transform.position;
        }
    }
}
