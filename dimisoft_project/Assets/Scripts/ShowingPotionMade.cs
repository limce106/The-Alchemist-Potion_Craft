using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowingPotionMade : MonoBehaviour
{
    // ĵ���� ���� ���� ���� �̹����� ����ִ� �޼ҵ�

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
            // Resources ���Ͽ� �ִ� ���� �̹����� �����ͼ� ���

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
        // �ݱ� ��ư ������ ���� �̹��� ����
        Destroy(Potion);
    }
}
