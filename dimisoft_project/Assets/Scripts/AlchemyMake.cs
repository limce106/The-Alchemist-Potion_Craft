using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyMake : MonoBehaviour
{
    public Item aItem;  // a재료
    public Item bItem;  // b재료
    public int aNum;           // a재료의 아이템 코드넘버
    public int bNum;           // b재료의 아이템 코드넘버

    public GameObject PotionMakePanel;  // 솥에 가까이 갔을 때 포션 만들 것인지 아닌지 보여주는 패널
    bool isReady = false;   // 포션 만드는 메소드를 쓸지 안 쓸지 결정하는 변수
    bool setReady = false;  // setStart()를 쓰기 위한 변수
    public GameObject StartTable;   // 포션 만들기 시작만 보여줌
    public GameObject WorkTable;    // 진짜 포션 만드는 거 패널
    public Text MakeText;   // 처음 "포션 만들기 시작!", 첫 단계만 보여주는 텍스트
    public Text GuideText;  // 포션 만드는 그 이후 단계들과 진행 상황 보여주고 어떤 포션이 완성되었는지 보여줌
    public GameObject MakingProgress;   // 스페이스바 누르고 포션 섞어지는 진행 상황 보여줌
    public GameObject CloseBtn;     // 포션 만들기 완성 후 활성화될 닫기 버튼

    private int recipe;
    public int potionNum = 0;

    [SerializeField]
    private GameObject View_item;

    [SerializeField]
    private GameObject noMaterialWarning;     // 재료가 숫자 슬롯에 안 채워져 있으면 재료가 없다고 경고함.

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

    // 숫자키 슬롯에 있던 재료를 aItem과 bItem에 옮김
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
        // 솥에 주위에 플레이어가 있고 플레이어가 포션을 들고 있으면 포션제작 못하게 함.
        // 솥에 가까이 갔을 때 putMaterial() 메소드로 재료를 aItem과 bItem에 넣고 만드는 패널을 킴

        if (collision.CompareTag("Pot"))
        {
            if(GameObject.Find("map_PotionManager").GetComponent<PotionManager>().Potion==null)
            {
                putMaterial();
                //PotionMakePanel.SetActive(true);
            }
        }
    }

    // 여기서 부터
    public void YesButtonClick()    // 솥에 가까이 갔을 때 뜨는 패널의 "예" 버튼
    {
        if((aItem==null||bItem==null)||(aItem==null&&bItem==null))
        {
            // 재료가 부족하거나 없을 경우 경고 텍스트 1초간 띄웠다가 없어지고 
            // 스타트 패널 안 뜸.
            noMaterialWarning.SetActive(true);
            StartCoroutine(WarningHide());
            StartTable.SetActive(false);
            // 플레이어들은 No 버튼 눌러서 나가서 인벤토리에 있던 재료를 1, 2번 슬롯에 넣던가 해야 함.
        }
        else
        {
            print("Yes 버튼 누름");
            PotionMakePanel.SetActive(false);   // 초반에 묻는 패널 비활성화하고
            StartTable.SetActive(true);         // 스타트 패널 활성화

            MakeText.text = "포션 만들기 시작!";   // 스타트 패널 위에 만들기 시작 텍스트 띄움
            setReady = true;
        }
    }
    public void NoButtonClick()     // "아니오" 버튼
    {
        PotionMakePanel.SetActive(false);
        // 아니오를 누르면 포션 만들기 취소함
    }

    public void CloseButtonClick()     // "닫기" 버튼
    {
        if(1 <= potionNum && potionNum <= 10)
        {
            GameObject.Find("map_PotionManager").GetComponent<GivePotion>().setPotionNum(potionNum);

            // 닫기 버튼 누르면 상단에 만든 포션이 뜸
            GameObject.Find("ViewPotionManager").GetComponent<PotionView>().ViewPotion();

            // 맵에 포션 프리팹 생김
            GameObject.Find("map_PotionManager").GetComponent<PotionManager>().DropPotion();

            ResetItemNum();

            GameObject.Find("ShowPotionManager").GetComponent<ShowingPotionMade>().destoryPotion();

            // 포션 만들기 종료 전에 bool 타입 변수들 false로 초기화 
            isReady = false;
            setReady = false;

            // 가이드텍스트 내용 비어있게 초기화
            GuideText.text = "";

            WorkTable.SetActive(false);
            // 닫기를 누르면 포션 만들기 종료
        }
        else
        {
            ResetItemNum();

            // 포션 만들기 종료 전에 bool 타입 변수들 false로 초기화 
            isReady = false;
            setReady = false;

            // 가이드텍스트 내용 비어있게 초기화
            GuideText.text = "";

            WorkTable.SetActive(false);
            // 닫기를 누르면 포션 만들기 종료
        }
    }
    // 여기까지 버튼 작동 스크립트

    IEnumerator setStart()
    {
        // "포션 만들기 시작!" 패널 띄우고  1초 후에 첫번째 단계 띄워줌
        yield return new WaitForSeconds(1.0f);

        MakeText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 160);
        MakeText.text = "<size=45><color=red>A 재료</color>를 넣으려면 <color=blue>1</color>를 눌러주세요</size>";

        isReady = true;
    }

    public void start_make()
    {
        if (isReady)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartTable.SetActive(false);    // 시작하면 스타트 패널 비활성화하고
                CloseBtn.SetActive(false);
                WorkTable.SetActive(true);      // 작업하는 패널 활성화
                View_item.SetActive(true);
                print("A 재료 넣기");
                aNum = aItem.itemCode;

                // 솥에 재료를 넣으면 슬롯에 보이는 아이템 파괴
                theAlchemySlot.ClearSlot(theAlchemySlot.alchemySlots_1);
                theAlchemySlot.SetColor_1(0);

                View_item.GetComponent<MakingViewItem>().ViewOn = true;

                GuideText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 160);
                GuideText.text = "<size=45><color=red>B 재료</color>를 넣으려면 <color=blue>2</color>를 눌러주세요</size>";
                //print("B 재료를 넣으려면 '2'를 눌러주세요");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                print("B 재료 넣기");
                bNum = bItem.itemCode;

                theAlchemySlot.ClearSlot(theAlchemySlot.alchemySlots_2);
                theAlchemySlot.SetColor_2(0);

                GuideText.text = "<size=45>섞으려면 <color=blue>스페이스바</color>를 눌러주세요</size>";
                //print("섞으려면 '스페이스바'를 눌러주세요");
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                // View_item에 뜨는 이미지 초기화.
                View_item.GetComponent<MakingViewItem>().ResetItemImage();

                View_item.SetActive(false);
                print("섞기");
                GuideText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 30);
                GuideText.text = "<size=50>섞는 중...</size>";
                StartCoroutine(makeStart());
                MakingProgress.SetActive(true);
            }
        }
    }

    IEnumerator makeStart()
    {
        // 포션 섞고 완성 직전 5초 시간 걸리게 함
        yield return new WaitForSeconds(3f);

        makePotion(aNum, bNum);
    }

    void makePotion(int Anum, int Bnum)
    {
        recipe = Anum * 10 + Bnum;

        // 끝나면 진행 슬라이더바 시간 초기화
        GameObject.Find("Making Progress").GetComponent<MakingProgressSlider>().slTimer.value = 0f;
        MakingProgress.SetActive(false);

        GuideText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 165);
        switch (recipe)
        {
            // 여기부터 올바른 레시피
            case 17:
                potionNum = 1;
                GuideText.text = "소화 포션 완성";
                print("레시피 17번의 포션 완성!");
                break;
            case 18:
                potionNum = 2;
                GuideText.text = "성장 촉진 포션 완성";
                print("레시피 18번의 포션 완성!");
                break;
            case 21:
                potionNum = 3;
                GuideText.text = "회복 포션 완성";
                print("레시피 21번의 포션 완성!");
                break;
            case 32:
                potionNum = 4;
                GuideText.text = "마나 포션 완성";
                print("레시피 32번의 포션 완성!");
                break;
            case 36:
                potionNum = 5;
                GuideText.text = "빙결 포션 완성";
                print("레시피 36번의 포션 완성!");
                break;
            case 43:
                potionNum = 6;
                GuideText.text = "집중력 강화 포션 완성";
                print("레시피 43번의 포션 완성!");
                break;
            case 44:
                potionNum = 7;
                GuideText.text = "지혈 포션 완성";
                print("레시피 44번의 포션 완성!");
                break;
            case 49:
                potionNum = 8;
                GuideText.text = "진실 포션 완성";
                print("레시피 49번의 포션 완성!");
                break;
            case 60:
                potionNum = 9;
                GuideText.text = "행운 포션 완성";
                print("레시피 50번의 포션 완성!");
                break;
            case 55:
                potionNum = 10;
                GuideText.text = "수면 포션 완성";
                print("레시피 55번의 포션 완성!");
                break;

            // 레시피 이외의 조합이면 실패
            default:
                potionNum = 0;
                GuideText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 30);
                GuideText.text = "<size=60><color=red>실패!</color></size>";
                print("실패!");
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
