using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // 플레이어 게임 오브젝트
    private GameObject Player; 
    // 어디로 이동하는지
    public string MoveTo;

    private void Awake() 
    {
        Player = GameObject.Find("Player");
    }
    
    void OnTriggerEnter(Collider other)
    {
        // 플레이어의 최근 사용한 Door를 MoveTo로 지정
        PlayerDataManager.instance.movePoint = MoveTo;
        // Door 콜라이더에 플레이어가 닿으면 해당 씬으로 이동
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
