using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMng : MonoBehaviour
{
    public GameObject harvestButton;
    public GameObject plantButton;
    public GameObject buttonParent_2;
    public GameObject plantObject_3;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(plantObject_3);
        GameObject newButton = Instantiate(plantButton, buttonParent_2.transform);
        harvestButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
