using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    // ĵ���� ���� ���� ���� �̹����� ����ִ� �޼ҵ�

    // ���ɹ̿��� ���ǳѹ� �޾ƿ��� ���� ��
    private AlchemyMake alchemy;

    // ���Ƕ�� �ڸ�
    [SerializeField]
    private GameObject PotionPosition;

    // ���� ��� �� �ʿ��� ��
    private string potion_Num;
    public GameObject Potion;

    void Start()
    {
        alchemy = FindObjectOfType<AlchemyMake>();
    }

    // �ʿ� ���� ������ ����.
    public void DropPotion()
    {
        if (1 <= alchemy.potionNum && alchemy.potionNum <= 10)
        {
            // Resources ���Ͽ� �ִ� ���� ������ �����ͼ� �÷��̾� �տ� ���

            potion_Num = "" + alchemy.potionNum;

            GameObject temp = Resources.Load("PotionPrefab/POTION" + potion_Num) as GameObject;
            Potion = Instantiate(temp);

            Potion.transform.SetParent(PotionPosition.transform);

            Transform rc = Potion.GetComponent<Transform>();
            rc.localPosition = Vector2.zero;
        }
    }

    public void destoryPotion()
    {
        // ���� ������ ����
        Destroy(Potion);
    }
}
