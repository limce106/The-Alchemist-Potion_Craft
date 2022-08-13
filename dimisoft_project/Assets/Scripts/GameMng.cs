using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    public GameObject customer1;
    public GameObject customer2;
    public GameObject customer3;
    public GameObject customer4;

    Vector3 customerPos1 = new Vector3(-5, 0.5f, 0);
    Vector3 customerPos2 = new Vector3(0, 0.5f, 0);
    Vector3 customerPos3 = new Vector3(5, 0.5f, 0);

    public int customerCount = 0;

    public bool isEmpty1 = true;
    public bool isEmpty2 = true;
    public bool isEmpty3 = true;

    public Level curLevel = Level.Level1;

    public enum Level
    {
        Level1,
        Level2,
        Level3
    }

    // Start is called before the first frame update
    void Start()
    {
        // 첫 번째 단계: 손님 발생 빈도 조절 (35 초에 1 명)
        if (PlayerDataManager.instance.level == 1)
        {
            InvokeRepeating("putCustomer", 0, 35);
        }

        // 두 번째 단계: 손님 발생 빈도 조절 (30 초에 1 명)
        else if (PlayerDataManager.instance.level == 2)
        {
            InvokeRepeating("putCustomer", 0, 30);
        }

        // 세 번째 단계: 손님 발생 빈도 조절 (25 초에 1 명)
        else
        {
            InvokeRepeating("putCustomer", 0, 25);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void putCustomer()
    {
        if (customerCount >= 3)
        {
            print("손님이 전부 찼습니다. 오류.");
        }
        else if (isEmpty1)
        {
            initCustomer(customerPos1).GetComponent<CustomerCtrl>().customerPos = 1;
            customerCount++;
            isEmpty1 = false;
        }
        else if (isEmpty2)
        {
            initCustomer(customerPos2).GetComponent<CustomerCtrl>().customerPos = 2;
            customerCount++;
            isEmpty2 = false;
        }
        else
        {
            initCustomer(customerPos3).GetComponent<CustomerCtrl>().customerPos = 3;
            customerCount++;
            isEmpty3 = false;
        }
    }

    GameObject initCustomer(Vector3 vec)
    {
        float rand = Random.Range(-10f, 10f);
        GameObject gameobj;
        if (rand >= 5f) {
            gameobj = Instantiate(customer1, vec, Quaternion.identity);
            gameobj.transform.LookAt(new Vector3(0, 0, -100));
        }
        else if(rand >= 0f)
        {
            gameobj = Instantiate(customer2, vec, Quaternion.identity);
            gameobj.transform.LookAt(new Vector3(0, 0, -100));
        }
        else if(rand >= -5f)
        {
            gameobj = Instantiate(customer3, vec, Quaternion.identity);
            gameobj.transform.LookAt(new Vector3(0, 0, -100));
        }
        else
        {
            gameobj = Instantiate(customer4, vec, Quaternion.identity);
            gameobj.transform.LookAt(new Vector3(0, 0, -100));
        }

        return gameobj;
    }
}
