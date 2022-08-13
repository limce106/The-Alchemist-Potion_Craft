using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerCtrl : MonoBehaviour
{
    // 주문이 완료되었는지를 나타내는 변수
    bool isComplete = false;
    // 주문 성공 시 손님이 지불하는 액수 저장하는 변수
    int reward;
    // 손님이 주문할 물약의 주문 번호
    int potionNum;
    int receivedPotionNum = 0;
    // 손님이 몇 번째 자리에 있는지 나타내는 변수
    public int customerPos;

    GameObject gameMng;

    GameObject canvas;
    Text orderText;
    Slider timeSlider;

    float limitTime = 40;

    Talk Talk;
    TalkManager talkManager;
    string customerOrder;

    // Start is called before the first frame update
    void Start()
    {
        // 이후 스테이지(레벨) 나타내는 변수 구현되면 조건문으로 포션 등장 범위를 나누어준다
        // 1레벨에서는 -10 ~ -8
        // 2레벨에서는 -10 ~ -6....
        gameMng = GameObject.FindGameObjectWithTag("GameMng");
        canvas = gameObject.transform.GetChild(0).gameObject;
        orderText = canvas.transform.GetChild(0).gameObject.GetComponentInChildren<Text>();
        timeSlider = canvas.transform.Find("Slider").GetComponent<Slider>();

        Talk = GameObject.Find("TalkManager").GetComponent<Talk>();
        talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();

        // 레벨 별로 등장하는 포션 변화
        if (PlayerDataManager.instance.level == 1)
        {
            Talk.CtalkIndex = Random.Range(0, 18);
        }
        else if (PlayerDataManager.instance.level == 2)
        {
            Talk.CtalkIndex = Random.Range(0, 24);
        }
        else if (PlayerDataManager.instance.level == 3)
        {
            Talk.CtalkIndex = Random.Range(0, 30);
        }

        potionNum = Talk.CtalkIndex;
        customerOrder = talkManager.customerTalk[Talk.CtalkIndex];
        orderText.text = customerOrder;
    }

    // Update is called once per frame
    void Update()
    {
        limitTime -= Time.deltaTime;

        timeSlider.value = limitTime;

        /*
        switch (potionNum)
        {
            case -10:
                orderText.text = "포션 1번을 제조해야 합니다.";
                break;
            case -9:
                orderText.text = "포션 2번을 제조해야 합니다.";
                break;
            case -8:
                orderText.text = "포션 3번을 제조해야 합니다.";
                break;
        }
        */

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            orderSuccess();
        }
        if (limitTime <= 0)
        {
            customerGone();
        }
    }

    // 주문이 완료되지 않은 채로 일정 시간 지나면 실행할 함수
    public void customerGone()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().controlFameDown();
        orderText.text = "손님이 가 버렸습니다!";
        gameMng.GetComponent<GameMng>().customerCount--;
        if (customerPos == 1)
        {
            gameMng.GetComponent<GameMng>().isEmpty1 = true;
        }
        else if (customerPos == 2)
        {
            gameMng.GetComponent<GameMng>().isEmpty2 = true;
        }
        else if (customerPos == 3)
        {
            gameMng.GetComponent<GameMng>().isEmpty3 = true;
        }
        Destroy(gameObject, 10);
    }

    // 주문이 완료되면 실행할 함수
    public void orderSuccess()
    {
        int coin = 0;

        print("포션을 성공적으로 만들었습니다.");
        // 플레이어 소지금 변수에 reward 추가하는 코드 넣을 것
        // 포션 종류별로 코인 다르게 지급할 수 있도록 함 

        if ((potionNum % 3 + 1) != receivedPotionNum)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().controlFameDown();
            orderText.text = "잘못된 포션을 전달했습니다!";
            gameMng.GetComponent<GameMng>().customerCount--;
            if (customerPos == 1)
            {
                gameMng.GetComponent<GameMng>().isEmpty1 = true;
            }
            else if (customerPos == 2)
            {
                gameMng.GetComponent<GameMng>().isEmpty2 = true;
            }
            else if (customerPos == 3)
            {
                gameMng.GetComponent<GameMng>().isEmpty3 = true;
            }
            Destroy(gameObject, 10);
        }
        else
        {
            //level 1
            // 소화 포션인 경우
            if ((potionNum >= 0) && (potionNum <= 2))
            {
                coin = 100;
            }
            // 성장 촉진 포션인 경우
            else if ((potionNum >= 3) && (potionNum <= 5))
            {
                coin = 150;
            }
            // 회복 포션인 경우
            else if ((potionNum >= 6) && (potionNum <= 8))
            {
                coin = 200;
            }
            // 마나 포션인 경우
            else if ((potionNum >= 9) && (potionNum <= 11))
            {
                coin = 250;
            }
            // 빙결 포션인 경우
            else if ((potionNum >= 12) && (potionNum <= 14))
            {
                coin = 300;
            }
            // 집중력 강화 포션인 경우
            else if ((potionNum >= 15) && (potionNum <= 17))
            {
                coin = 350;
            }

            // level 2
            // 지혈 포션인 경우
            else if ((potionNum >= 18) && (potionNum <= 20))
            {
                coin = 400;
            }
            // 진실 포션인 경우
            else if ((potionNum >= 21) && (potionNum <= 23))
            {
                coin = 450;
            }

            // level 3
            // 행운 포션인 경우
            else if ((potionNum >= 24) && (potionNum <= 26))
            {
                coin = 500;
            }
            // 수면 포션인 경우
            else if ((potionNum >= 27) && (potionNum <= 29))
            {
                coin = 550;
            }

            GameObject.Find("Player").GetComponent<PlayerController>().controlFameUp(coin);
        }

        gameMng.GetComponent<GameMng>().customerCount--;

        if (customerPos == 1)
        {
            gameMng.GetComponent<GameMng>().isEmpty1 = true;
        }
        else if (customerPos == 2)
        {
            gameMng.GetComponent<GameMng>().isEmpty2 = true;
        }
        else if (customerPos == 3)
        {
            gameMng.GetComponent<GameMng>().isEmpty3 = true;
        }

        Destroy(gameObject);
    }

    public void inputPotion(int potionNum)
    {
        receivedPotionNum = potionNum;
    }
}
