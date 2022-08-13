using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Talk : MonoBehaviour
{
    // 대화창에 띄울 텍스트
    public Text talkText;
    // 대화창
    public GameObject talkPanel;
    // 대화데이터베이스 인덱스
    public int CtalkIndex;
    // public int CtalkIndex2;
    // public int CtalkIndex3;
    // PlayerController 스크립트
    PlayerController pc;
    TalkManager talkManager;
    BuyItem bi;
    GameObject coinPanel;
    Text cointext;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        if(SceneManager.GetActiveScene().name == "Player_customer" || SceneManager.GetActiveScene().name == "ItemStore")
        {
            talkPanel = GameObject.Find("talkPanel");
            talkText = GameObject.Find("talkText").GetComponent<Text>();
            talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
            // 처음에는 대화창, 텍스트 비활성화
            talkPanel.SetActive(false);
            talkText.gameObject.SetActive(false);
        }
        if(SceneManager.GetActiveScene().name == "ItemStore")
        {
            bi = GameObject.Find("StoreNpc").GetComponent<BuyItem>();
            talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
        }
        // 아래 코드는 손님을 생성하는 코드에 옮길 예정
        // (손님마다 대화의 내용이 다르게 하기 위함)
        // CtalkIndex = Random.Range(0, talkManager.customerTalk.Count);
    }

    void Update()
    {
        
    }

    public void ClickTalk(List<string>talk) {
        // 플레이어의 가게 안에서
        // if(SceneManager.GetActiveScene().name == "Player")
        // {
        //     // 대화데이터 인덱스, 대화 랜덤출력
        //     // talkText.text = talk[CtalkIndex];
        // }
        // // 상점 안에서
        // if(SceneManager.GetActiveScene().name == "Store")
        // {
        //     if(Input.GetKeyDown(KeyCode.P))
        //     {
        //         talkText.text = talkManager.storeTalk[1];
        //     }
        //     else
        //     {
        //         //talkText.text = talkManager.storeTalk[0];
        //     }
        // }
        // 플레이어가 손님을 클릭하면
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 마우스가 태그가 Npc인 게임 오브젝트 위에 있고 클릭하면
        if (Physics.Raycast(ray, out hit))
        {
            if ((hit.transform.gameObject.tag == "Npc") && (Input.GetMouseButtonDown(0)))
            {
                if(SceneManager.GetActiveScene().name == "ItemStore")
                {
                    talkText.text = talkManager.storeTalk[0];
                }
                pc.isMoving = false;
                // 대화창, 텍스트 활성화
                talkPanel.SetActive(true);
                talkText.gameObject.SetActive(true);
            }
            else
            {
                if(SceneManager.GetActiveScene().name == "Player")
                {
                    pc.MouseMove();
                }
            }
        }
        // 스페이스바를 누르면 대화창 꺼짐
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(SceneManager.GetActiveScene().name == "ItemStore")
            {
                pc.isMoving = true;
                bi.buttonOnOff(false);
                talkPanel.SetActive(false);
                talkText.gameObject.SetActive(false);
                bi.spacebar.gameObject.SetActive(false);
            }
        }
    }
}
