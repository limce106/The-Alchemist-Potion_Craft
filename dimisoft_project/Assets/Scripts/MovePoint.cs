using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    // 씬 이동 후 도착한 지점의 이름
    public string doorPoint;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        if (doorPoint == PlayerDataManager.instance.movePoint)
        {
            // 플레이어의 위치는 movePoint로 지정
            player.transform.position = this.transform.position;
        }
    }
}
