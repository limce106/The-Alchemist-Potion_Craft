using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BuyItem : MonoBehaviour
{
    Talk talk;
    TalkManager tm;
    Button b1;
    Button b2;
    Button b3;
    Button b4;
    Button b5;
    public GameObject talkPanel;
    // Npc 대사
    public Text talkText;
    // 선택지 버튼들의 부모 오브젝트
    GameObject buttons;
    // 버튼 클릭 횟수 누적 방지를 위한 변수
    int clickCount = 0;
    // 아이템명
    string itemName;
    // 스페이스바 안내 텍스트
    public Text spacebar;
    int price = 0;

    // npc 앞에 아이템 생성
    public GameObject itemObject;
    // npc 앞에서 생성될 아이템 위치
    private GameObject itemPosition;
    
    void Start()
    {
        //talkPanel = GameObject.Find("talkPanel");
        talk = GameObject.Find("Player").GetComponent<Talk>();
        tm = GameObject.Find("TalkManager").GetComponent<TalkManager>();
        buttons = GameObject.Find("Buttons");
        // talkText = GameObject.Find("talkText").GetComponent<Text>();
        //spacebar = GameObject.Find("Spacebar").GetComponent<Text>();
        // 선택지 비활성화
        buttonOnOff(false);
        spacebar.gameObject.SetActive(false);
        
         // 위치를 npc의 위치로 설정
        itemPosition = this.gameObject;
    }

    void Update()
    {
        //clickItems();
        // 대화 텍스트가 상점 주인의 대화 데이터베이스이고 패널이 켜져있을 때
        if(talkPanel.activeSelf == true)
        {
            spacebar.gameObject.SetActive(true);
            buttons.transform.GetChild(0).gameObject.SetActive(true);
            buttons.transform.GetChild(1).gameObject.SetActive(true);

            if(PlayerDataManager.instance.level >= 3)
            {
                buttons.transform.GetChild(3).gameObject.SetActive(true);
                buttons.transform.GetChild(4).gameObject.SetActive(true);
                buttons.transform.GetChild(2).gameObject.SetActive(true);
                b3 = GameObject.Find("Button3").GetComponent<Button>();
                b4 = GameObject.Find("Button4").GetComponent<Button>();
                b5 = GameObject.Find("Button5").GetComponent<Button>();
            }
            else if(PlayerDataManager.instance.level >= 2)
            {
                buttons.transform.GetChild(2).gameObject.SetActive(true);
                b3 = GameObject.Find("Button3").GetComponent<Button>();
            }
        }
        else
        {
            return;
        }
    }

    // 선택지를 활성화/비활성화 하는 함수
    public void buttonOnOff(bool OnOFF)
    {
        for(int i = 0;i<5;i++)
        {
            buttons.transform.GetChild(i).gameObject.SetActive(OnOFF);
        }

    }

    public void Buy()
    {
        if(!Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerDataManager.instance.coin >= price)
            {
                StartCoroutine(OkBuy(itemName));
               // if(EventSystem.current.currentSelectedGameObject == buttons.transform.GetChild(0))
                // {
                //     itemName = "빨간 버섯";
                //     price = 100;
                // }
                // else if(EventSystem.current.currentSelectedGameObject == buttons.transform.GetChild(1))
                // {
                //     itemName = "단풍잎";
                //     price = 200;
                // }
                // else if(EventSystem.current.currentSelectedGameObject == buttons.transform.GetChild(2))
                // {
                //     itemName = "연꽃";
                //     price = 300;
                // }
                // else if(EventSystem.current.currentSelectedGameObject == buttons.transform.GetChild(3))
                // {
                //     itemName = "검은 깃털";
                //     price = 400;
                // }
                // else if(EventSystem.current.currentSelectedGameObject == buttons.transform.GetChild(4))
                // {
                //     itemName = "불탄 나뭇가지";
                //     price = 500;
                // }
                PlayerDataManager.instance.coin -= price;
                if(PlayerDataManager.instance.coin <= 0)
                {
                    PlayerDataManager.instance.coin = 0;
                }
                Debug.Log("가격: " + price + "코인: " + PlayerDataManager.instance.coin);
                //clickCount = 0;
            }
            // 플레이어의 코인이 가격보다 적으면 Npc의 대사를 구매할 수 없다는 대사로 바꿈
            else
            {
                //talkText = GameObject.Find("talkText").GetComponent<Text>();
                talkText.text = tm.storeTalk[1];
            }
        }
    }

    public void button1()
    {
        itemName = "빨간 버섯";
        price = 100;
        Buy();
        // 빨간 버섯 인벤토리에 추가

        // Resources 파일에 있는 ItemPrefab 가져와서 띄움

        GameObject temp = Resources.Load("ItemPrefab/Store/Red Mushroom") as GameObject;
        itemObject = Instantiate(temp);

        itemObject.transform.SetParent(itemPosition.transform);

        Transform rc = itemObject.GetComponent<Transform>();
        rc.localPosition = Vector2.zero;
    }
    public void button2()
    {
        itemName = "단풍잎";
        price = 200;
        Buy();
        // 단풍잎 인벤토리에 추가

        // Resources 파일에 있는 ItemPrefab 가져와서 띄움

        GameObject temp = Resources.Load("ItemPrefab/Store/Maple Leaf") as GameObject;
        itemObject = Instantiate(temp);

        itemObject.transform.SetParent(itemPosition.transform);

        Transform rc = itemObject.GetComponent<Transform>();
        rc.localPosition = Vector2.zero;
    }
    public void button3()
    {
        itemName = "연꽃";
        price = 300;
        Buy();
        // 연꽃 인벤토리에 추가

        // Resources 파일에 있는 ItemPrefab 가져와서 띄움

        GameObject temp = Resources.Load("ItemPrefab/Store/Lotus") as GameObject;
        itemObject = Instantiate(temp);

        itemObject.transform.SetParent(itemPosition.transform);

        Transform rc = itemObject.GetComponent<Transform>();
        rc.localPosition = Vector2.zero;
    }
    public void button4()
    {
        itemName = "검은 깃털";
        price = 400;
        Buy();
        // 검은 깃털 인벤토리에 추가

        // Resources 파일에 있는 ItemPrefab 가져와서 띄움

        GameObject temp = Resources.Load("ItemPrefab/Store/Black Feather") as GameObject;
        itemObject = Instantiate(temp);

        itemObject.transform.SetParent(itemPosition.transform);

        Transform rc = itemObject.GetComponent<Transform>();
        rc.localPosition = Vector2.zero;
    }
    public void button5()
    {
        itemName = "불탄 나뭇가지";
        price = 500;
        Buy();
        // 불탄 나뭇가지 인벤토리에 추가

        // Resources 파일에 있는 ItemPrefab 가져와서 띄움

        GameObject temp = Resources.Load("ItemPrefab/Store/Burnt Branch") as GameObject;
        itemObject = Instantiate(temp);

        itemObject.transform.SetParent(itemPosition.transform);

        Transform rc = itemObject.GetComponent<Transform>();
        rc.localPosition = Vector2.zero;
    }

    // 버튼을 클릭했을 때 함수
    // void clickItems()
    // {
    //     // 대화 텍스트가 상점 주인의 대화 데이터베이스이고 패널이 켜져있을 때
    //     if(talkPanel.activeSelf == true && clickCount == 0)
    //     {
    //         spacebar.gameObject.SetActive(true);
    //         buttons.transform.GetChild(0).gameObject.SetActive(true);
    //         buttons.transform.GetChild(1).gameObject.SetActive(true);
    //         b1 = GameObject.Find("Button1").GetComponent<Button>();
    //         b2 = GameObject.Find("Button2").GetComponent<Button>();
    //         //talkText = GameObject.Find("talkText").GetComponent<Text>();

    //         if(PlayerDataManager.instance.level >= 3)
    //         {
    //             buttons.transform.GetChild(3).gameObject.SetActive(true);
    //             buttons.transform.GetChild(4).gameObject.SetActive(true);
    //             buttons.transform.GetChild(2).gameObject.SetActive(true);
    //             b3 = GameObject.Find("Button3").GetComponent<Button>();
    //             b4 = GameObject.Find("Button4").GetComponent<Button>();
    //             b5 = GameObject.Find("Button5").GetComponent<Button>();
    //         }
    //         else if(PlayerDataManager.instance.level >= 2)
    //         {
    //             buttons.transform.GetChild(2).gameObject.SetActive(true);
    //             b3 = GameObject.Find("Button3").GetComponent<Button>();
    //         }
            
    //         if(Input.GetMouseButtonDown(0))
    //         {
    //             b1.onClick.AddListener(() => 
    //             { 
    //                 itemName = "레몬 밤";
    //                 Buy(100); 
    //                 clickCount++;
    //                 // 레몬 밤 인벤토리에 추가
    //             });

    //             b2.onClick.AddListener(() => 
    //             { 
    //                 itemName = "커먼 세이지";
    //                 Buy(200); 
    //                 clickCount++;
    //                 // 커먼 세이지 인벤토리에 추가
    //             });

    //             if(PlayerDataManager.instance.level >= 3)
    //             {
    //                 b3.onClick.AddListener(() => 
    //                 { 
    //                     itemName = "페퍼민트";
    //                     Buy(300); 
    //                     clickCount++;
    //                     // 페퍼민트 인벤토리에 추가
    //                 });
    //                 b4.onClick.AddListener(() => 
    //                 { 
    //                     itemName = "로즈마리 꽃";
    //                     Buy(400); 
    //                     clickCount++;
    //                     // 로즈마리 꽃 인벤토리에 추가
    //                 });
    //                 b5.onClick.AddListener(() => 
    //                 { 
    //                     itemName = "타임";
    //                     Buy(500); 
    //                     clickCount++;
    //                     // 타임 인벤토리에 추가
    //                 });
    //             }
    //             else if(PlayerDataManager.instance.level >= 2)
    //             {
    //                 b3.onClick.AddListener(() => 
    //                 { 
    //                     itemName = "페퍼민트";
    //                     Buy(300); 
    //                     clickCount++;
    //                     // 페퍼민트 인벤토리에 추가
    //                 });
    //             }
    //         }
    //     }
    //     else
    //     {
    //         return;
    //     }
    // }

    IEnumerator OkBuy(string item)
    {
        clickCount++;
        talkText.text = item + "을(를) 구매했습니다.";
        yield return new WaitForSeconds(2);
        talkText.text = tm.storeTalk[0];
    } 
}
