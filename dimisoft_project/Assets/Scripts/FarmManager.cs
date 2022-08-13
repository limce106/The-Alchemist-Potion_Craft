using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public GameObject PlantPanel; //심기 패널
    public GameObject HerbPanel; //약초 심을 거 정할 패널
    public GameObject SliderPanel; //프로그래스 패널
    public GameObject HarvestPanel; //수확 패널
    bool isPlant = false; 
    bool setPlant = false;

    // Start is called before the first frame update
    void Start()
    {
        PlantPanel.SetActive(false); 
        HerbPanel.SetActive(false); 
        SliderPanel.SetActive(false); 
        HarvestPanel.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        //if(setPlant)
        //{
        //    StartCoroutine(setStart);
        //    start_plant();
        //}
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Farm"))
        {
            PlantPanel.SetActive(true);
        }    
    }

    public void PlantButtonClick()
    {
        PlantPanel.SetActive(false);
        HerbPanel.SetActive(true);
    }
}
