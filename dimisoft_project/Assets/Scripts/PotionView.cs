using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionView : MonoBehaviour
{
    // 캔버스 위에 만든 포션 이미지를 띄워주는 메소드

    private AlchemyMake alchemy;

    [SerializeField]
    private GameObject Potion_view;     // 패널 자체를 활성화 비활성화 하기 위한 컴포넌트
    [SerializeField]
    private GameObject potionView;      // 여기에 포션 이미지가 올라갈 자리

    private string potion_Num;
    GameObject Potion;
    public int tmepNum;

    void Start()
    {
        Potion_view.SetActive(false);
        alchemy = FindObjectOfType<AlchemyMake>();
    }

    public void ViewPotion()
    {
        if (1 <= alchemy.potionNum && alchemy.potionNum <= 10)
        {
            // 패널 활성화
            Potion_view.SetActive(true);

            // Resources 파일에 있는 포션 이미지를 가져와서 띄움
            tmepNum = alchemy.potionNum;
            potion_Num = "_" + tmepNum;

            GameObject temp = Resources.Load("PotionPrefab_sprite/POTION" + potion_Num) as GameObject;
            Potion = Instantiate(temp);

            Potion.transform.SetParent(potionView.transform);

            RectTransform rc = Potion.GetComponent<RectTransform>();
            rc.localPosition = Vector2.zero;
        }
    }

    public void hidePanel()
    {
        // 포션 건네주면 패널 비활성화하고 포션 이미지 없애기.
        Potion_view.SetActive(false);
        Destroy(Potion);
    }
}
