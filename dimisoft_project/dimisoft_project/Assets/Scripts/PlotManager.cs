using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    bool isPlanted = false;
    GameObject plant;
    //BoxCollider plantCollider;

    public GameObject[] plantStages;
    int plantStage = 0;
    float timeBtwStages = 2f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<GameObject>();
        //plantCollider = transform.GetChild(0).GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlanted)
        {
            timer -= Time.deltaTime;

            if(timer < 0 && plantStage<=plantStages.Length-1)
            {
                timer = timeBtwStages;
                plantStage++;
                UpdatePlant();
            }
        }
    }

    private void OnMouseDown()
    {
        if(isPlanted)
        {
            if(plantStage < plantStages.Length-1)
            {
                Harvest();
            }
        }
        else
        {
            Plant();
        }

        Debug.Log("Clicked");
    }

    void Harvest()
    {
        Debug.Log("Harvested");
        isPlanted = false;
        plant.SetActive(false);
    }

    void Plant()
    {
        Debug.Log("Planted");
        isPlanted = true;
        plantStage = 0;
        UpdatePlant();
        timer = timeBtwStages;
        plant.SetActive(true);
    }

    void UpdatePlant()
    {
        plant = plantStages[plantStage];
        //plantCollider.size = plant.transform.localScale;
        //plantCollider.contactOffset = new Vector3(0, plant.size.y, 0);
    }
}
