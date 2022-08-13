using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow_up : MonoBehaviour
{
    int start;
    GameObject Cube2_1;
    GameObject Cube2_2;
    GameObject Cube2_3;
    // Start is called before the first frame update
    void Start()
    {
        start = 0;
        Cube2_1 = transform.GetChild(0).gameObject;
        Cube2_2 = transform.GetChild(1).gameObject;
        Cube2_3 = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        start = PlantMng.plant;
        growing_up(start);
    }

    void growing_up(int start)
    {
        if(start >= 0 && start < 45)
        {
            if(Cube2_1.activeSelf == false)
            {
                Cube2_1.SetActive(true);
            }
        }
        else if(start >= 45 && start < 90)
        {
            if(Cube2_1.activeSelf == true)
            {
                Cube2_1.SetActive(false);
            }
            else if(Cube2_2.activeSelf == false)
            {
                Cube2_2.SetActive(true);
            }
        }
        else if(start >= 90)
        {
            if(Cube2_2.activeSelf == true)
            {
                Cube2_2.SetActive(false);
            }
            else if(Cube2_3.activeSelf == false)
            {
                Cube2_3.SetActive(true);
            }
        }
    }
}
