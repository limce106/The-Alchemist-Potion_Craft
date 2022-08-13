using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float LimitTime;
    public Text text_Timer;
    int min;
    float sec;
    public GameObject plantObject_1;
    public GameObject plantObject_3;
    public GameObject harvestButton;
    public GameObject buttonParent;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Harvest", 0.1f, 0.001f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Harvest()
    {
        LimitTime -= Time.deltaTime;

        if (LimitTime >= 60f)
        {
            min = (int)LimitTime / 60;
            sec = LimitTime % 60;
            text_Timer.text = "남은 시간 : " + min + "분" + (int)sec + "초";
        }

        if (LimitTime < 60f)
        {
            text_Timer.text = "남은 시간 : " + (int)LimitTime + "초";
        }

        if (LimitTime <= 0)
        {
            text_Timer.text = "수확해주세요!";
            CancelInvoke("Harvest");
            Instantiate(plantObject_3, Vector3.right, Quaternion.identity);
            Destroy(plantObject_1);
            GameObject newButton = Instantiate(harvestButton, buttonParent.transform);
            harvestButton.SetActive(true);
        }
    }
}
