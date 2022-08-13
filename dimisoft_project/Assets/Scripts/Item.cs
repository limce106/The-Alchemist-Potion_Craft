using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]

public class Item : ScriptableObject
{
    public string itemName;     //아이템의 이름
    [TextArea]
    public string itemDesc;     // 아이템의 설명
    public ItemType itemType;   //아이템의 유형
    public Sprite itemImage;    //아이템의 이미지
    public GameObject itemPrefab; //아이템의 프리팹
    public int itemCode;        //아이템의 코드

    public enum ItemType
    {
        Material_Basic,     // 기본적으로 가지고 있는 재료 (베이스 재료이기에 인벤토리에서 사용 시 1번키로 이동)
        Material_Store,     // 상점에서 얻을 수 있는 재료 (베이스 재료+상점 재료이기에 사용 시 2번키로 이동)
        Material_Garden,    // 텃밭에서 얻을 수 있는 재료 (베이스 재료+밭 재료이기에 사용 시 2번키로 이동)
        Etc                 // 기타
    }
}