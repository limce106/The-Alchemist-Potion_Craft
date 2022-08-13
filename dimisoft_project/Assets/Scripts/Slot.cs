using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;       // ȹ���� ������.
    public int itemCount = 0;   // ȹ���� �������� ����.
    public Image itemImage;  // �������� �̹���.

    // �ʿ��� ������Ʈ
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private SlotToolTip theSlotToolTip;

    // ���ǿ����� �����Ŵ����� ��� �������̸� �����ϰ� �ߴµ�
    // �츮�ʿ����� ��Ḧ ��Ŭ���ϸ� ���ʴ�� 1��Ű, 2��Ű�� ���� �ϴ� �������� �����غ���

    // 1��Ű, 2��Ű�� �� ��Ḧ AlchemySlotController�� �̵��ϰ� �ϱ�
    private AlchemySlotController theAlchemySlot;

    void Start()
    {
        theSlotToolTip = FindObjectOfType<SlotToolTip>();
        theAlchemySlot = FindObjectOfType<AlchemySlotController>();
    }

    // �̹����� ���� ����.
    private void SetColor(float _alpha)
    {
        // ������ ������ ���� �̹��� ���İ� 0���� ����� �޼ҵ�.
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // ������ ȹ��.
    public void AddItem(Item _item, int _count = 1)
    {
        // _count=1 �⺻���� ������ �ѹ� ���� ������ �Ѱ��� �԰� ��
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        go_CountImage.SetActive(true);
        text_Count.text = itemCount.ToString();

        SetColor(1);
    }

    // ������ ���� ����.
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // ���� �ʱ�ȭ.
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
        itemImage.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                Item _tempItem1; Item _tempItem2;

                if (item.itemType == Item.ItemType.Material_Basic)  //1��Ű�� �̵�
                {
                    if (theAlchemySlot.alchemySlots_1.item == null)
                    {
                        _tempItem1 = item;
                        theAlchemySlot.AddItem_AlchemySlot_1(_tempItem1);
                        Debug.Log(item.itemName + " �� 1��Ű�� �ֱ�.");
                        SetSlotCount(-1);
                    }
                    else
                    {
                        Debug.Log("�̹� 1��Ű�� ��ᰡ �ֽ��ϴ�.");
                    }
                }
                else if (item.itemType == Item.ItemType.Material_Store || item.itemType == Item.ItemType.Material_Garden)   //2��Ű�� �̵�
                {
                    if (theAlchemySlot.alchemySlots_2.item == null)
                    {
                        _tempItem2 = item;
                        theAlchemySlot.AddItem_AlchemySlot_2(_tempItem2);
                        Debug.Log(item.itemName + " �� 2��Ű�� �ֱ�.");
                        SetSlotCount(-1);
                    }
                    else
                    {
                        Debug.Log("�̹� 2��Ű�� ��ᰡ �ֽ��ϴ�.");
                    }
                }
                else
                {
                    Debug.Log(item.itemName + " �� ����߽��ϴ�.");
                    SetSlotCount(-1);
                }
            }
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);

            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
            ChangeSlot();
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }

    // ���콺�� ���Կ� �� �� �ߵ�.
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            theSlotToolTip.ShowToolTip(item, transform.position);
    }

    // ���Կ��� �������� �� �ߵ�.
    public void OnPointerExit(PointerEventData eventData)
    {
        theSlotToolTip.HideToolTip();
    }
}
