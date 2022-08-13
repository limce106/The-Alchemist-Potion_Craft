using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionView : MonoBehaviour
{
    // ĵ���� ���� ���� ���� �̹����� ����ִ� �޼ҵ�

    private AlchemyMake alchemy;

    [SerializeField]
    private GameObject Potion_view;     // �г� ��ü�� Ȱ��ȭ ��Ȱ��ȭ �ϱ� ���� ������Ʈ
    [SerializeField]
    private GameObject potionView;      // ���⿡ ���� �̹����� �ö� �ڸ�

    private string potion_Num;
    GameObject Potion;
    public int tmepNum;

    void Start()
    {
        Potion_view.SetActive(false);
        alchemy = FindObjectOfType<AlchemyMake>();
    }

    public void ViewPotion()
    {
        if (1 <= alchemy.potionNum && alchemy.potionNum <= 10)
        {
            // �г� Ȱ��ȭ
            Potion_view.SetActive(true);

            // Resources ���Ͽ� �ִ� ���� �̹����� �����ͼ� ���
            tmepNum = alchemy.potionNum;
            potion_Num = "_" + tmepNum;

            GameObject temp = Resources.Load("PotionPrefab_sprite/POTION" + potion_Num) as GameObject;
            Potion = Instantiate(temp);

            Potion.transform.SetParent(potionView.transform);

            RectTransform rc = Potion.GetComponent<RectTransform>();
            rc.localPosition = Vector2.zero;
        }
    }

    public void hidePanel()
    {
        // ���� �ǳ��ָ� �г� ��Ȱ��ȭ�ϰ� ���� �̹��� ���ֱ�.
        Potion_view.SetActive(false);
        Destroy(Potion);
    }
}
