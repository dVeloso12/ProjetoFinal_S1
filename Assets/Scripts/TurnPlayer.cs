using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnPlayer : MonoBehaviour
{

    [SerializeField] float mouseSensivity;
    [SerializeField] Camera firstPersonCamera;


    float mouseRotationX, mouseRotationY;

    // Start is called before the first frame update
    void Start()
    {
        mouseRotationX = 0f;
        mouseRotationY = 0f;
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

        mouseRotationX += mouseX;
        mouseRotationY -= mouseY;


        mouseRotationY = Mathf.Clamp(mouseRotationY, -90f, 90f);


        transform.rotation = Quaternion.Euler(0f, mouseRotationX, 0f);


        firstPersonCamera.transform.rotation = Quaternion.Euler(mouseRotationY, mouseRotationX, 0f);
    }
}
