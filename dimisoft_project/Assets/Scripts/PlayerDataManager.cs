using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance = null;
    public static PlayerDataManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake() 
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 코인
    public int coin = 100000;
    // 명성
    public float fame = 10;
    // 레벨
    public int level = 1;
    // 현재 이동 포인트
    public string movePoint;
}
