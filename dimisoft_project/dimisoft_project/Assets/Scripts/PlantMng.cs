using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMng : MonoBehaviour
{
    public static int plant;

    void Start()
    {
        Debug.Log("plant: " + plant);
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
       {
            plant += 5;
            KeyDown_Space();
       }    
    }

    void KeyDown_Space()
    {
        Debug.Log("Ω√¿€: " + plant);
    }
}
