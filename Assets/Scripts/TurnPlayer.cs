using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnPlayer : MonoBehaviour
{

    [SerializeField] float mouseSensivity;
    [SerializeField] Camera firstPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
    }

    void Rotate()
    {

        float mouseX, mouseY;

        mouseX = Input.GetAxisRaw("Mouse X") * mouseSensivity * Time.fixedDeltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensivity * Time.fixedDeltaTime;


        transform.eulerAngles += new Vector3(0f, mouseX, 0f);



        float rotationX = Mathf.Clamp(firstPersonCamera.transform.eulerAngles.x - mouseY , -90f, 90f);

        Debug.Log("Camera rotation : " + rotationX);

        firstPersonCamera.transform.eulerAngles = new Vector3(rotationX, firstPersonCamera.transform.eulerAngles.y, firstPersonCamera.transform.eulerAngles.z);
    }
}
