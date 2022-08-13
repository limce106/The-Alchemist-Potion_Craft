using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public GameObject PlantPanel; //�ɱ� �г�
    public GameObject HerbPanel; //���� ���� �� ���� �г�
    public GameObject SliderPanel; //���α׷��� �г�
    public GameObject HarvestPanel; //��Ȯ �г�
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
