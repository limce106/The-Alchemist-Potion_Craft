using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    //private bool pickupActivated = false;   // ���� ������ �� true.

    // �ʿ��� ������Ʈ.
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
        Debug.Log(item.GetItem().itemName + " ȹ���߽��ϴ�");
        ItemInfoAppear();
        item.DestroyItem();
        StartCoroutine(InfoDisappear_term());
        theInventory.AcquireItem(item.GetItem());
    }

    // �׳� ������ �ٷ� �ڵ� �ݱ� �Ǿ ȹ���ϸ� ��µǰ� �� ����
    private void ItemInfoAppear()
    {
        //pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "<color=blue>" + item.GetItem().itemName + "</color>" + " ȹ�� ";
    }

    private void InfoDisappear()
    {
        //pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    IEnumerator InfoDisappear_term()
    {
        // ������ ȹ���� Ȯ�� ���� ������ 2�� �Ŀ� ������� ��
        yield return new WaitForSeconds(2f);

        actionText.gameObject.SetActive(false);
    }

}