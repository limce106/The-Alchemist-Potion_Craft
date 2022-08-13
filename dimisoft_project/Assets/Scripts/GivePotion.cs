using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePotion : MonoBehaviour
{
    int potionNum;
    GameObject gameMng;

    GameObject player;
    GameObject customer1;
    GameObject customer2;
    GameObject customer3;


    private void Start()
    {
        player = GameObject.Find("Player");
        gameMng = GameObject.FindGameObjectWithTag("GameMng");
        customer1 = gameMng.GetComponent<GameMng>().customer1;
        customer2 = gameMng.GetComponent<GameMng>().customer2;
        customer3 = gameMng.GetComponent<GameMng>().customer3;
    }

    void Update()
    {
        // 손님 중심 좌표
       Vector3 c1 = new Vector3(-5, 0.5f, 0);
        Vector3 c2 = new Vector3(0, 0.5f, 0);
        Vector3 c3 = new Vector3(5, 0.5f, 0);
        
        // 플레이어 중심 좌표
        Vector3 p = player.transform.position;

        Vector3 dir1 = p - c1;
        Vector3 dir2 = p - c2;
        Vector3 dir3 = p - c3;

        // 플레이어와 손님들 사이의 거리
        float d1 = dir1.magnitude;
        float d2 = dir2.magnitude;
        float d3 = dir3.magnitude;

        // 주변에 있을 경우 givePotion(); 메소드 실행됨
        if (d1 < 2f)
        {
            if (potionNum >= 1 && potionNum <= 10)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    GameObject.Find("ViewPotionManager").GetComponent<PotionView>().hidePanel();
                    GameObject.Find("map_PotionManager").GetComponent<PotionManager>().destoryPotion();

                    customer1.GetComponent<CustomerCtrl>().inputPotion(potionNum);

                    Debug.Log("손님1에게 포션을 전달했습니다.");
                }
            }
        }
        else if(d2 < 2f)
        {
            if (potionNum >= 1 && potionNum <= 10)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    GameObject.Find("ViewPotionManager").GetComponent<PotionView>().hidePanel();
                    GameObject.Find("map_PotionManager").GetComponent<PotionManager>().destoryPotion();

                    customer2.GetComponent<CustomerCtrl>().inputPotion(potionNum);

                    Debug.Log("손님2에게 포션을 전달했습니다.");
                }
            };
        }
        else if(d3 < 2f)
        {
            if (potionNum >= 1 && potionNum <= 10)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    GameObject.Find("ViewPotionManager").GetComponent<PotionView>().hidePanel();
                    GameObject.Find("map_PotionManager").GetComponent<PotionManager>().destoryPotion();

                    customer3.GetComponent<CustomerCtrl>().inputPotion(potionNum);

                    Debug.Log("손님3에게 포션을 전달했습니다.");
                }
            }
        }
    }

    public void setPotionNum(int num)
    {
        potionNum = num;
    }    

    //void givePotion()
    //{
    //    if (potionNum >= 1 && potionNum <= 10)
    //    {
    //        if (Input.GetKeyDown(KeyCode.G))
    //        {
    //            GameObject.Find("ViewPotionManager").GetComponent<PotionView>().hidePanel();
    //            GameObject.Find("map_PotionManager").GetComponent<PotionManager>().destoryPotion();

    //            GameObject.Find("customer Ctrl").GetComponent<CustomerCtrl>().inputPotion(potionNum);

    //            Debug.Log("손님에게 포션을 전달했습니다.");
    //        }
    //    }
    //}


}
