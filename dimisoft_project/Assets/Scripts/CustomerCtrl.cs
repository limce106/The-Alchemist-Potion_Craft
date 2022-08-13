using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerCtrl : MonoBehaviour
{
    // �ֹ��� �Ϸ�Ǿ������� ��Ÿ���� ����
    bool isComplete = false;
    // �ֹ� ���� �� �մ��� �����ϴ� �׼� �����ϴ� ����
    int reward;
    // �մ��� �ֹ��� ������ �ֹ� ��ȣ
    int potionNum;
    int receivedPotionNum = 0;
    // �մ��� �� ��° �ڸ��� �ִ��� ��Ÿ���� ����
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
        // ���� ��������(����) ��Ÿ���� ���� �����Ǹ� ���ǹ����� ���� ���� ������ �������ش�
        // 1���������� -10 ~ -8
        // 2���������� -10 ~ -6....
        gameMng = GameObject.FindGameObjectWithTag("GameMng");
        canvas = gameObject.transform.GetChild(0).gameObject;
        orderText = canvas.transform.GetChild(0).gameObject.GetComponentInChildren<Text>();
        timeSlider = canvas.transform.Find("Slider").GetComponent<Slider>();

        Talk = GameObject.Find("TalkManager").GetComponent<Talk>();
        talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();

        // ���� ���� �����ϴ� ���� ��ȭ
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
                orderText.text = "���� 1���� �����ؾ� �մϴ�.";
                break;
            case -9:
                orderText.text = "���� 2���� �����ؾ� �մϴ�.";
                break;
            case -8:
                orderText.text = "���� 3���� �����ؾ� �մϴ�.";
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

    // �ֹ��� �Ϸ���� ���� ä�� ���� �ð� ������ ������ �Լ�
    public void customerGone()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().controlFameDown();
        orderText.text = "�մ��� �� ���Ƚ��ϴ�!";
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

    // �ֹ��� �Ϸ�Ǹ� ������ �Լ�
    public void orderSuccess()
    {
        int coin = 0;

        print("������ ���������� ��������ϴ�.");
        // �÷��̾� ������ ������ reward �߰��ϴ� �ڵ� ���� ��
        // ���� �������� ���� �ٸ��� ������ �� �ֵ��� �� 

        if ((potionNum % 3 + 1) != receivedPotionNum)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().controlFameDown();
            orderText.text = "�߸��� ������ �����߽��ϴ�!";
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
            // ��ȭ ������ ���
            if ((potionNum >= 0) && (potionNum <= 2))
            {
                coin = 100;
            }
            // ���� ���� ������ ���
            else if ((potionNum >= 3) && (potionNum <= 5))
            {
                coin = 150;
            }
            // ȸ�� ������ ���
            else if ((potionNum >= 6) && (potionNum <= 8))
            {
                coin = 200;
            }
            // ���� ������ ���
            else if ((potionNum >= 9) && (potionNum <= 11))
            {
                coin = 250;
            }
            // ���� ������ ���
            else if ((potionNum >= 12) && (potionNum <= 14))
            {
                coin = 300;
            }
            // ���߷� ��ȭ ������ ���
            else if ((potionNum >= 15) && (potionNum <= 17))
            {
                coin = 350;
            }

            // level 2
            // ���� ������ ���
            else if ((potionNum >= 18) && (potionNum <= 20))
            {
                coin = 400;
            }
            // ���� ������ ���
            else if ((potionNum >= 21) && (potionNum <= 23))
            {
                coin = 450;
            }

            // level 3
            // ��� ������ ���
            else if ((potionNum >= 24) && (potionNum <= 26))
            {
                coin = 500;
            }
            // ���� ������ ���
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
