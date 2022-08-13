using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AlchemySlotController : MonoBehaviour
{
    public Slot alchemySlots_1;    // 숫자키 슬롯. 1번키
    public Slot alchemySlots_2;    // 숫자키 슬롯. 2번키

    [SerializeField] private Text text1;    // 1번키 숫자
    [SerializeField] private Text text2;    // 2번키 숫자

    // 숫자키 슬롯에 재료가 있는 상태일 때 우클릭을 하면 인벤토리로 재료가 이동하기 위한 컴포넌트
    public Inventory theInventory;

    // Update is called once per frame
    void Update()
    {
        if (alchemySlots_1.item == null)
        {
            text1.text = "1";
        }
        if (alchemySlots_2.item == null)
        {
            text2.text = "2";
        }
    }

    // 1번키 슬롯
    // 이미지의 투명도 조절.
    public void SetColor_1(float _alpha)
    {
        // 슬롯이 없으면 슬롯 이미지 알파값 0으로 만드는 메소드.
        Color color = alchemySlots_1.itemImage.color;
        color.a = _alpha;
        alchemySlots_1.itemImage.color = color;
    }

    // 숫자키 슬롯용 아이템 획득 메소드
    public void AddItem_AlchemySlot_1(Item _item)
    {
        alchemySlots_1.item = _item;
        alchemySlots_1.itemImage.sprite = alchemySlots_1.item.itemImage;

        text1.text = "";
        SetColor_1(1);
    }


    // 2번키 슬롯
    public void SetColor_2(float _alpha)
    {
        // 슬롯이 없으면 슬롯 이미지 알파값 0으로 만드는 메소드.
        Color color = alchemySlots_2.itemImage.color;
        color.a = _alpha;
        alchemySlots_2.itemImage.color = color;
    }

    // 숫자키 슬롯용 아이템 획득 메소드
    public void AddItem_AlchemySlot_2(Item _item)
    {
        alchemySlots_2.item = _item;
        alchemySlots_2.itemImage.sprite = alchemySlots_2.item.itemImage;

        text2.text = "";
        SetColor_2(1);
    }

    // 재료가 있을 때 우클릭하면 슬롯 초기화
    public void ClearSlot(Slot slot)
    {
        slot.item = null;
        slot.itemCount = 0;
        slot.itemImage.sprite = null;
        //slot.SetColor(0);

        //slot.text_Count.text = "0";
        //slot.go_CountImage.SetActive(false);
        slot.itemImage.gameObject.SetActive(true);
    }

    public void resetButtonOnClick()
    {
        Item _tempItem1; Item _tempItem2;

        if (alchemySlots_1.item != null)
        {
            _tempItem1 = alchemySlots_1.item;
            theInventory.AcquireItem(_tempItem1);

            ClearSlot(alchemySlots_1);
            SetColor_1(0);
        }
        if (alchemySlots_2.item != null)
        {
            _tempItem2 = alchemySlots_2.item;
            theInventory.AcquireItem(_tempItem2);

            ClearSlot(alchemySlots_2);
            SetColor_2(0);
        }
    }
}
