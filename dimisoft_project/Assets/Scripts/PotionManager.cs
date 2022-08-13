using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    // 캔버스 위에 만든 포션 이미지를 띄워주는 메소드

    // 알케미에서 포션넘버 받아오기 위한 거
    private AlchemyMake alchemy;

    // 포션띄울 자리
    [SerializeField]
    private GameObject PotionPosition;

    // 포션 띄울 때 필요한 것
    private string potion_Num;
    public GameObject Potion;

    void Start()
    {
        alchemy = FindObjectOfType<AlchemyMake>();
    }

    // 맵에 포션 프리팹 생김.
    public void DropPotion()
    {
        if (1 <= alchemy.potionNum && alchemy.potionNum <= 10)
        {
            // Resources 파일에 있는 포션 프리팹 가져와서 플레이어 앞에 띄움

            potion_Num = "" + alchemy.potionNum;

            GameObject temp = Resources.Load("PotionPrefab/POTION" + potion_Num) as GameObject;
            Potion = Instantiate(temp);

            Potion.transform.SetParent(PotionPosition.transform);

            Transform rc = Potion.GetComponent<Transform>();
            rc.localPosition = Vector2.zero;
        }
    }

    public void destoryPotion()
    {
        // 포션 프리팹 제거
        Destroy(Potion);
    }
}
