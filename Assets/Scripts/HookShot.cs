using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HookShot : MonoBehaviour
{

    PlayerInput playerInput;
    public Camera _camera;

    Vector3 hookPos,MovePos,startpos;

    //private CharacterController controller;

    bool activeHook = false;



    float[] mult = new float[3];

    private CharacterController controller;

    public GameObject hook;

    public Transform origin;

    public float speed, dist;
    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<CharacterController>();

        controller = GetComponent<CharacterController>();

        startpos += new Vector3(0, 0.5f, 0);

        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        //playerInput.Player.Hook.performed += HookActivate;

        playerInput.Player.Hook.performed += HookShoot;
    }

    // Update is called once per frame
    void Update()
    {
       
        if(activeHook)
            controller.Move((MovePos.normalized) * speed);
        //controller.Move(()*speed);

        //Debug.Log((MovePos - hookPos).magnitude);

        

        if (activeHook)
        {
            //Debug.Log("Magnitude"+((transform.position) - hookPos).magnitude);
            //if (((transform.position) - hookPos).magnitude < dist)
            //{
            //    activeHook = false;
            //    Debug.Log("Off Dist");
            //    Clean();
            //}

            mult[0] = (transform.position.x - startpos.x) / MovePos.x;
            mult[1] = (transform.position.y - startpos.y) / MovePos.y;
            mult[2] = (transform.position.z - startpos.z) / MovePos.z;


            if (mult[0] >= 0 || mult[1] >=0 || mult[2] >= 0)
            {
                activeHook = false;
                Debug.Log("OFF Plus");
                Clean();
            }

            
        }
    }
    public void HookActivate(InputAction.CallbackContext obj)
    {

        hookPos = Vector3.zero;
        

        Ray EyeRay = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        Physics.Raycast(EyeRay, out hit, 30);

        Debug.Log("Ponto "+hit.point);

        hookPos = hit.point;

        if (hookPos == Vector3.zero)
        {
            activeHook = false;
            return;
        }

        Debug.DrawRay(_camera.transform.position, _camera.transform.forward *20,Color.red,5);

        MovePos =hit.point- (transform.position);

        startpos = hit.point;

        activeHook = true;

    }

    void Clean()
    {
        controller.Move(Vector3.zero);

        startpos = Vector3.zero;
        MovePos = Vector3.zero;
        hookPos = Vector3.zero;

        mult[0] = 0;
        mult[1] = 0;
        mult[2] = 0;
    }

    public void ActivateHook (Vector3 pos)
    {
        MovePos = pos - (transform.position);

        startpos = pos;

        activeHook = true;


    }

    public void HookShoot (InputAction.CallbackContext obj)
    {
        Instantiate(hook, origin.position, origin.rotation,transform);
    }
}
