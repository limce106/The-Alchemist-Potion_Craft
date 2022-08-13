using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;       // 획득한 아이템.
    public int itemCount = 0;   // 획득한 아이템의 개수.
    public Image itemImage;  // 아이템의 이미지.

    // 필요한 컴포넌트
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private SlotToolTip theSlotToolTip;

    // 강의에서는 웨폰매니저로 장비 아이템이면 장착하게 했는데
    // 우리쪽에서는 재료를 우클릭하면 차례대로 1번키, 2번키로 들어가게 하는 방향으로 수정해보기

    // 1번키, 2번키에 들어갈 재료를 AlchemySlotController로 이동하게 하기
    private AlchemySlotController theAlchemySlot;

    void Start()
    {
        theSlotToolTip = FindObjectOfType<SlotToolTip>();
        theAlchemySlot = FindObjectOfType<AlchemySlotController>();
    }

    // 이미지의 투명도 조절.
    private void SetColor(float _alpha)
    {
        // 슬롯이 없으면 슬롯 이미지 알파값 0으로 만드는 메소드.
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 아이템 획득.
    public void AddItem(Item _item, int _count = 1)
    {
        // _count=1 기본으로 아이템 한번 먹을 때마다 한개씩 먹게 함
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        go_CountImage.SetActive(true);
        text_Count.text = itemCount.ToString();

        SetColor(1);
    }

    // 아이템 개수 조정.
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 슬롯 초기화.
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

                if (item.itemType == Item.ItemType.Material_Basic)  //1번키로 이동
                {
                    if (theAlchemySlot.alchemySlots_1.item == null)
                    {
                        _tempItem1 = item;
                        theAlchemySlot.AddItem_AlchemySlot_1(_tempItem1);
                        Debug.Log(item.itemName + " 을 1번키에 넣기.");
                        SetSlotCount(-1);
                    }
                    else
                    {
                        Debug.Log("이미 1번키에 재료가 있습니다.");
                    }
                }
                else if (item.itemType == Item.ItemType.Material_Store || item.itemType == Item.ItemType.Material_Garden)   //2번키로 이동
                {
                    if (theAlchemySlot.alchemySlots_2.item == null)
                    {
                        _tempItem2 = item;
                        theAlchemySlot.AddItem_AlchemySlot_2(_tempItem2);
                        Debug.Log(item.itemName + " 을 2번키에 넣기.");
                        SetSlotCount(-1);
                    }
                    else
                    {
                        Debug.Log("이미 2번키에 재료가 있습니다.");
                    }
                }
                else
                {
                    Debug.Log(item.itemName + " 을 사용했습니다.");
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

    // 마우스가 슬롯에 들어갈 때 발동.
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            theSlotToolTip.ShowToolTip(item, transform.position);
    }

    // 슬롯에서 빠져나갈 때 발동.
    public void OnPointerExit(PointerEventData eventData)
    {
        theSlotToolTip.HideToolTip();
    }
}
