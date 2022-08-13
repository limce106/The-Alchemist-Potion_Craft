using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;  // 인벤토리 열었을 때 플레이어가 움직이거나 카메라 움직일 수 없게 함

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    // 슬롯들.
    private Slot[] slots;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        go_InventoryBase.SetActive(false);
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count);
                    Debug.Log("null X 확인 후 AddItem(_item, _count)");
                    return;
                }
            }
            else if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                Debug.Log("null 확인 후 AddItem(_item, _count)");
                return;
            }
        }
    }
}