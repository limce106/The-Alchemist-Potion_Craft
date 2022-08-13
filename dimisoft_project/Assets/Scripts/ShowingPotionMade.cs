using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowingPotionMade : MonoBehaviour
{
    // 캔버스 위에 만든 포션 이미지를 띄워주는 메소드

    private AlchemyMake alchemy;

    [SerializeField]
    private GameObject ShowPotion;

    private string potion_Num;
    GameObject Potion;

    void Start()
    {
        alchemy = FindObjectOfType<AlchemyMake>();
    }

    public void showPotion()
    {
        if (1 <= alchemy.potionNum && alchemy.potionNum <= 10)
        {
            // Resources 파일에 있는 포션 이미지를 가져와서 띄움

            potion_Num = "_" + alchemy.potionNum;

            GameObject temp = Resources.Load("PotionPrefab_sprite/POTION" + potion_Num) as GameObject;
            Potion = Instantiate(temp);

            Potion.transform.SetParent(ShowPotion.transform);

            RectTransform rc = Potion.GetComponent<RectTransform>();
            rc.localPosition = Vector2.zero;
        }
    }

    public void destoryPotion()
    {
        // 닫기 버튼 누르면 포션 이미지 제거
        Destroy(Potion);
    }
}
