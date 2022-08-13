using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;
    private float currentCameraRotationY = 0;

    [SerializeField]
    private Camera theCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraRoation();
    }

    void CameraRoation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        float cameraRotationY = yRotation * lookSensitivity;
        currentCameraRotationY += cameraRotationY;

        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensitivity;
        currentCameraRotationX += cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, -currentCameraRotationY, 0f);
    }
}
