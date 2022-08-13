using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMng : MonoBehaviour
{
    public GameObject plantObj;
    public Button button_1;

    public void Start()
    {
        button_1.gameObject.SetActive(true);
        plantObj.SetActive(true);
    }

    public void OnClick()
    {
        if(button_1.gameObject == true)
        {
            Debug.Log("수확했습니다.");
            Destroy(plantObj);
        }
    }
}
