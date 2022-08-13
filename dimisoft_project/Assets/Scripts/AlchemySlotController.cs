using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AlchemySlotController : MonoBehaviour
{
    public Slot alchemySlots_1;    // ����Ű ����. 1��Ű
    public Slot alchemySlots_2;    // ����Ű ����. 2��Ű

    [SerializeField] private Text text1;    // 1��Ű ����
    [SerializeField] private Text text2;    // 2��Ű ����

    // ����Ű ���Կ� ��ᰡ �ִ� ������ �� ��Ŭ���� �ϸ� �κ��丮�� ��ᰡ �̵��ϱ� ���� ������Ʈ
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

    // 1��Ű ����
    // �̹����� ���� ����.
    public void SetColor_1(float _alpha)
    {
        // ������ ������ ���� �̹��� ���İ� 0���� ����� �޼ҵ�.
        Color color = alchemySlots_1.itemImage.color;
        color.a = _alpha;
        alchemySlots_1.itemImage.color = color;
    }

    // ����Ű ���Կ� ������ ȹ�� �޼ҵ�
    public void AddItem_AlchemySlot_1(Item _item)
    {
        alchemySlots_1.item = _item;
        alchemySlots_1.itemImage.sprite = alchemySlots_1.item.itemImage;

        text1.text = "";
        SetColor_1(1);
    }


    // 2��Ű ����
    public void SetColor_2(float _alpha)
    {
        // ������ ������ ���� �̹��� ���İ� 0���� ����� �޼ҵ�.
        Color color = alchemySlots_2.itemImage.color;
        color.a = _alpha;
        alchemySlots_2.itemImage.color = color;
    }

    // ����Ű ���Կ� ������ ȹ�� �޼ҵ�
    public void AddItem_AlchemySlot_2(Item _item)
    {
        alchemySlots_2.item = _item;
        alchemySlots_2.itemImage.sprite = alchemySlots_2.item.itemImage;

        text2.text = "";
        SetColor_2(1);
    }

    // ��ᰡ ���� �� ��Ŭ���ϸ� ���� �ʱ�ȭ
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
