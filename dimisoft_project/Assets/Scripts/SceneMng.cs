using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMng : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Store")
        {
            SceneManager.LoadScene("ItemStore");
            Destroy(this.gameObject);
            // this.transform.position = new Vector3(-2, 0.5f, 1);
        }
        else if(other.tag == "House")
        {
            SceneManager.LoadScene("House");
            Destroy(this.gameObject);
            // this.transform.position = new Vector3(-2, 0.5f, -4);
        }
    }
}
