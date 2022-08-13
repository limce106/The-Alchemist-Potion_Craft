using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // �÷��̾� ���� ������Ʈ
    private GameObject Player; 
    // ���� �̵��ϴ���
    public string MoveTo;

    private void Awake() 
    {
        Player = GameObject.Find("Player");
    }
    
    void OnTriggerEnter(Collider other)
    {
        // �÷��̾��� �ֱ� ����� Door�� MoveTo�� ����
        PlayerDataManager.instance.movePoint = MoveTo;
        // Door �ݶ��̴��� �÷��̾ ������ �ش� ������ �̵�
        if (other.gameObject.name == "Player")
        {
            if (MoveTo == "Store")
            {
                SceneManager.LoadScene("Store");
            }  
            else if (MoveTo == "Player_customer")
            {
                SceneManager.LoadScene("Player_customer");
            }  
        }
    }
}
