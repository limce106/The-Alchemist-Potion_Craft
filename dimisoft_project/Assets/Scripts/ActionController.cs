using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    //private bool pickupActivated = false;   // 습득 가능할 시 true.

    // 필요한 컴포넌트.
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theInventory;

    ItemPickUp item;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Item"))
        {
            item = collision.GetComponent<ItemPickUp>();

            CanPickUp();
        }
        else
            InfoDisappear();
    }

    private void CanPickUp()
    {
        Debug.Log(item.GetItem().itemName + " 획득했습니다");
        ItemInfoAppear();
        item.DestroyItem();
        StartCoroutine(InfoDisappear_term());
        theInventory.AcquireItem(item.GetItem());
    }

    // 그냥 아이템 바로 자동 줍기 되어서 획득하면 출력되게 할 것임
    private void ItemInfoAppear()
    {
        //pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "<color=blue>" + item.GetItem().itemName + "</color>" + " 획득 ";
    }

    private void InfoDisappear()
    {
        //pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    IEnumerator InfoDisappear_term()
    {
        // 아이템 획득한 확인 문구 나오고 2초 후에 사라지게 함
        yield return new WaitForSeconds(2f);

        actionText.gameObject.SetActive(false);
    }

}