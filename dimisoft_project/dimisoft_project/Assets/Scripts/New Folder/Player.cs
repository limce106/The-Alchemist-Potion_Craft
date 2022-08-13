using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Space mySpace;
    Vector3 prePos;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 deltaPos = Input.mousePosition - prePos;
            deltaPos *= (Time.deltaTime * 1.0f);

            transform.Translate(deltaPos.x, 0, deltaPos.y, mySpace);
        }

        prePos = Input.mousePosition;
    }
}
