using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    int Time;
    GameObject Cube3_1;
    GameObject Cube3_2;
    GameObject Cube3_3;

    public FieldMng getVariable1;
    public PanelMng getVariable2;

    void Start()
    {
        Time = 0;
    }
    void Update()
    {
        Invoke("plantgrow", 3.0f);
        Cube3_1 = transform.GetChild(0).gameObject;
        Cube3_2 = transform.GetChild(1).gameObject;
        Cube3_3 = transform.GetChild(2).gameObject;
    }

    void plantgrow()
    {
        Time += 1;
        //Debug.Log("Time: " + Time);

        if (Time >= 0 && Time < 30)
        {
            if (Cube3_1.activeSelf == false)
            {
                Cube3_1.SetActive(true);
            }
        }
        else if (Time >= 30 && Time < 60)
        {
            if (Cube3_1.activeSelf == true)
            {
                Cube3_1.SetActive(false);
            }
            else if (Cube3_2.activeSelf == false)
            {
                Cube3_2.SetActive(true);
            }
        }
        else if (Time >= 60)
        {
            if (Cube3_2.activeSelf == true)
            {
                Cube3_2.SetActive(false);
            }
            else if (Cube3_3.activeSelf == false)
            {
                Cube3_3.SetActive(true);
                Debug.Log("다 자랐습니다.");
                //getVariable1 = GameObject.FindWithTag("Garden").GetComponent("FieldMng") as FieldMng;
                //getVariable2 = GameObject.FindWithTag("Panel1").GetComponent("PanelMng") as PanelMng;
            }
        }
    }
}
