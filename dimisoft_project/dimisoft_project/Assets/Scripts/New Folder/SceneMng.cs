using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMng : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Town")
        {
            SceneManager.LoadScene("Town");
        }
        else if(other.tag == "House")
        {
            SceneManager.LoadScene("House");
        }
    }
}
