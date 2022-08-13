using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTown : MonoBehaviour
{
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        door();
    }

    void door()
    {
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000.0f))
        {
            if(hit.collider.gameObject.tag == "Door")
            {
                if(Input.GetMouseButton(0))
                {
                    SceneManager.LoadScene("HouseToTown");
                    Destroy(this.gameObject);
                    // this.transform.position = new Vector3(99, 0.5f, 156);
                    // this.transform.rotation = Quaternion.Euler(0, 180, 0);
                    // this.transform.localScale = new Vector3(5, 5, 5);
                }
            }
        }
    }
}
