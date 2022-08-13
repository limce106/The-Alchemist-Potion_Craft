using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyMake : MonoBehaviour
{
    public Item aItem;  // a���
    public Item bItem;  // b���
    public int aNum;           // a����� ������ �ڵ�ѹ�
    public int bNum;           // b����� ������ �ڵ�ѹ�

    public GameObject PotionMakePanel;  // �ܿ� ������ ���� �� ���� ���� ������ �ƴ��� �����ִ� �г�
    bool isReady = false;   // ���� ����� �޼ҵ带 ���� �� ���� �����ϴ� ����
    bool setReady = false;  // setStart()�� ���� ���� ����
    public GameObject StartTable;   // ���� ����� ���۸� ������
    public GameObject WorkTable;    // ��¥ ���� ����� �� �г�
    public Text MakeText;   // ó�� "���� ����� ����!", ù �ܰ踸 �����ִ� �ؽ�Ʈ
    public Text GuideText;  // ���� ����� �� ���� �ܰ��� ���� ��Ȳ �����ְ� � ������ �ϼ��Ǿ����� ������
    public GameObject MakingProgress;   // �����̽��� ������ ���� �������� ���� ��Ȳ ������
    public GameObject CloseBtn;     // ���� ����� �ϼ� �� Ȱ��ȭ�� �ݱ� ��ư

    private int recipe;
    public int potionNum = 0;

    [SerializeField]
    private GameObject View_item;

    [SerializeField]
    private GameObject noMaterialWarning;     // ��ᰡ ���� ���Կ� �� ä���� ������ ��ᰡ ���ٰ� �����.

    private AlchemySlotController theAlchemySlot;

    void Start()
    {
        PotionMakePanel.SetActive(false);
        StartTable.SetActive(false);
        WorkTable.SetActive(false);
        MakingProgress.SetActive(false);
        CloseBtn.SetActive(false);
        View_item.SetActive(false);
        noMaterialWarning.SetActive(false);
        //View_item = GameObject.Find("View_Item");
        theAlchemySlot = FindObjectOfType<AlchemySlotController>();
    }

    void Update()
    {
        if (setReady)
        {
            StartCoroutine(setStart());
        }
        start_make();
    }

    // ����Ű ���Կ� �ִ� ��Ḧ aItem�� bItem�� �ű�
    private void putMaterial()
    {
        if (theAlchemySlot.alchemySlots_1 != null && theAlchemySlot.alchemySlots_2 != null)
        {
            aItem = theAlchemySlot.alchemySlots_1.item;

            bItem = theAlchemySlot.alchemySlots_2.item;

            PotionMakePanel.SetActive(true);
        }
        //else
        //{
        //    noMaterialWarning.SetActive(true);
        //    StartCoroutine(WarningHide());
        //}

    }

    IEnumerator WarningHide()
    {
        yield return new WaitForSeconds(2.0f);

        noMaterialWarning.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // �ܿ� ������ �÷��̾ �ְ� �÷��̾ ������ ��� ������ �������� ���ϰ� ��.
        // �ܿ� ������ ���� �� putMaterial() �޼ҵ�� ��Ḧ aItem�� bItem�� �ְ� ����� �г��� Ŵ

        if (collision.CompareTag("Pot"))
        {
            if(GameObject.Find("map_PotionManager").GetComponent<PotionManager>().Potion==null)
            {
                putMaterial();
                //PotionMakePanel.SetActive(true);
            }
        }
    }

    // ���⼭ ����
    public void YesButtonClick()    // �ܿ� ������ ���� �� �ߴ� �г��� "��" ��ư
    {
        if((aItem==null||bItem==null)||(aItem==null&&bItem==null))
        {
            // ��ᰡ �����ϰų� ���� ��� ��� �ؽ�Ʈ 1�ʰ� ����ٰ� �������� 
            // ��ŸƮ �г� �� ��.
            noMaterialWarning.SetActive(true);
            StartCoroutine(WarningHide());
            StartTable.SetActive(false);
            // �÷��̾���� No ��ư ������ ������ �κ��丮�� �ִ� ��Ḧ 1, 2�� ���Կ� �ִ��� �ؾ� ��.
        }
        else
        {
            print("Yes ��ư ����");
            PotionMakePanel.SetActive(false);   // �ʹݿ� ���� �г� ��Ȱ��ȭ�ϰ�
            StartTable.SetActive(true);         // ��ŸƮ �г� Ȱ��ȭ

            MakeText.text = "���� ����� ����!";   // ��ŸƮ �г� ���� ����� ���� �ؽ�Ʈ ���
            setReady = true;
        }
    }
    public void NoButtonClick()     // "�ƴϿ�" ��ư
    {
        PotionMakePanel.SetActive(false);
        // �ƴϿ��� ������ ���� ����� �����
    }

    public void CloseButtonClick()     // "�ݱ�" ��ư
    {
        if(1 <= potionNum && potionNum <= 10)
        {
            GameObject.Find("map_PotionManager").GetComponent<GivePotion>().setPotionNum(potionNum);

            // �ݱ� ��ư ������ ��ܿ� ���� ������ ��
            GameObject.Find("ViewPotionManager").GetComponent<PotionView>().ViewPotion();

            // �ʿ� ���� ������ ����
            GameObject.Find("map_PotionManager").GetComponent<PotionManager>().DropPotion();

            ResetItemNum();

            GameObject.Find("ShowPotionManager").GetComponent<ShowingPotionMade>().destoryPotion();

            // ���� ����� ���� ���� bool Ÿ�� ������ false�� �ʱ�ȭ 
            isReady = false;
            setReady = false;

            // ���̵��ؽ�Ʈ ���� ����ְ� �ʱ�ȭ
            GuideText.text = "";

            WorkTable.SetActive(false);
            // �ݱ⸦ ������ ���� ����� ����
        }
        else
        {
            ResetItemNum();

            // ���� ����� ���� ���� bool Ÿ�� ������ false�� �ʱ�ȭ 
            isReady = false;
            setReady = false;

            // ���̵��ؽ�Ʈ ���� ����ְ� �ʱ�ȭ
            GuideText.text = "";

            WorkTable.SetActive(false);
            // �ݱ⸦ ������ ���� ����� ����
        }
    }
    // ������� ��ư �۵� ��ũ��Ʈ

    IEnumerator setStart()
    {
        // "���� ����� ����!" �г� ����  1�� �Ŀ� ù��° �ܰ� �����
        yield return new WaitForSeconds(1.0f);

        MakeText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 160);
        MakeText.text = "<size=45><color=red>A ���</color>�� �������� <color=blue>1</color>�� �����ּ���</size>";

        isReady = true;
    }

    public void start_make()
    {
        if (isReady)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartTable.SetActive(false);    // �����ϸ� ��ŸƮ �г� ��Ȱ��ȭ�ϰ�
                CloseBtn.SetActive(false);
                WorkTable.SetActive(true);      // �۾��ϴ� �г� Ȱ��ȭ
                View_item.SetActive(true);
                print("A ��� �ֱ�");
                aNum = aItem.itemCode;

                // �ܿ� ��Ḧ ������ ���Կ� ���̴� ������ �ı�
                theAlchemySlot.ClearSlot(theAlchemySlot.alchemySlots_1);
                theAlchemySlot.SetColor_1(0);

                View_item.GetComponent<MakingViewItem>().ViewOn = true;

                GuideText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 160);
                GuideText.text = "<size=45><color=red>B ���</color>�� �������� <color=blue>2</color>�� �����ּ���</size>";
                //print("B ��Ḧ �������� '2'�� �����ּ���");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                print("B ��� �ֱ�");
                bNum = bItem.itemCode;

                theAlchemySlot.ClearSlot(theAlchemySlot.alchemySlots_2);
                theAlchemySlot.SetColor_2(0);

                GuideText.text = "<size=45>�������� <color=blue>�����̽���</color>�� �����ּ���</size>";
                //print("�������� '�����̽���'�� �����ּ���");
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                // View_item�� �ߴ� �̹��� �ʱ�ȭ.
                View_item.GetComponent<MakingViewItem>().ResetItemImage();

                View_item.SetActive(false);
                print("����");
                GuideText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 30);
                GuideText.text = "<size=50>���� ��...</size>";
                StartCoroutine(makeStart());
                MakingProgress.SetActive(true);
            }
        }
    }

    IEnumerator makeStart()
    {
        // ���� ���� �ϼ� ���� 5�� �ð� �ɸ��� ��
        yield return new WaitForSeconds(3f);

        makePotion(aNum, bNum);
    }

    void makePotion(int Anum, int Bnum)
    {
        recipe = Anum * 10 + Bnum;

        // ������ ���� �����̴��� �ð� �ʱ�ȭ
        GameObject.Find("Making Progress").GetComponent<MakingProgressSlider>().slTimer.value = 0f;
        MakingProgress.SetActive(false);

        GuideText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 165);
        switch (recipe)
        {
            // ������� �ùٸ� ������
            case 17:
                potionNum = 1;
                GuideText.text = "��ȭ ���� �ϼ�";
                print("������ 17���� ���� �ϼ�!");
                break;
            case 18:
                potionNum = 2;
                GuideText.text = "���� ���� ���� �ϼ�";
                print("������ 18���� ���� �ϼ�!");
                break;
            case 21:
                potionNum = 3;
                GuideText.text = "ȸ�� ���� �ϼ�";
                print("������ 21���� ���� �ϼ�!");
                break;
            case 32:
                potionNum = 4;
                GuideText.text = "���� ���� �ϼ�";
                print("������ 32���� ���� �ϼ�!");
                break;
            case 36:
                potionNum = 5;
                GuideText.text = "���� ���� �ϼ�";
                print("������ 36���� ���� �ϼ�!");
                break;
            case 43:
                potionNum = 6;
                GuideText.text = "���߷� ��ȭ ���� �ϼ�";
                print("������ 43���� ���� �ϼ�!");
                break;
            case 44:
                potionNum = 7;
                GuideText.text = "���� ���� �ϼ�";
                print("������ 44���� ���� �ϼ�!");
                break;
            case 49:
                potionNum = 8;
                GuideText.text = "���� ���� �ϼ�";
                print("������ 49���� ���� �ϼ�!");
                break;
            case 60:
                potionNum = 9;
                GuideText.text = "��� ���� �ϼ�";
                print("������ 50���� ���� �ϼ�!");
                break;
            case 55:
                potionNum = 10;
                GuideText.text = "���� ���� �ϼ�";
                print("������ 55���� ���� �ϼ�!");
                break;

            // ������ �̿��� �����̸� ����
            default:
                potionNum = 0;
                GuideText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 30);
                GuideText.text = "<size=60><color=red>����!</color></size>";
                print("����!");
                break;
        }
        GameObject.Find("ShowPotionManager").GetComponent<ShowingPotionMade>().showPotion();

        CloseBtn.SetActive(true);
    }

    public void ResetItemNum()
    {
        aNum = 0;
        bNum = 0;
        recipe = 0;
        potionNum = 0;
    }

}
