using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMng2 : MonoBehaviour
{
    GameObject Cube3_1;
    public Button button1;
    public Button button2;

    public void Start()
    {
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
    }

    public void OnClick()
    {
        if (button1.gameObject == true)
        {
            Cube3_1.SetActive(true);

        }
        if (button2.gameObject == true)
        {
            Debug.Log("취소했습니다.");
        }
    }
}
