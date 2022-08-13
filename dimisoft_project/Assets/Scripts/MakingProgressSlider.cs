using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakingProgressSlider : MonoBehaviour
{
    public Slider slTimer;

    void Start()
    {
        slTimer = GetComponent<Slider>();
    }

    void Update()
    {
        if (slTimer.value <= 3.0f)
        {
            slTimer.value += Time.deltaTime;
        }
    }
}
