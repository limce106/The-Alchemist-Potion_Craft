using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmMng : MonoBehaviour
{
    public GameObject plantObject_1;
    public GameObject plantObject_3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddObject()
    {
        Instantiate(plantObject_1, Vector3.right, Quaternion.identity);
    }

    public void DestroyObject()
    {
        Destroy(plantObject_3);
    }
}
