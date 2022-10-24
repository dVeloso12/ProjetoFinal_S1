using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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

    public bool projectile;

    public float HookCooldown;

    float HookTimer=0;

    public TextMeshProUGUI Timer;
    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<CharacterController>();

        controller = GetComponent<CharacterController>();

        startpos += new Vector3(0, 0.5f, 0);

        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        dist = hook.GetComponent<HookGrappler>().Reach;

        if(!projectile)
            playerInput.Player.Hook.performed += HookActivate;
        else
        playerInput.Player.Hook.performed += HookShoot;

        Timer = GameObject.Find("HookC").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (HookTimer > 0)
        {
            HookTimer -= Time.deltaTime;
            Timer.text = Mathf.Round(HookTimer).ToString();
        }

        if(activeHook)
            controller.Move((MovePos.normalized) * speed);
        
        if (activeHook)
        {
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
        if (HookTimer <= 0)
        {
            hookPos = Vector3.zero;


            Ray EyeRay = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            Physics.Raycast(EyeRay, out hit, 30);

            Debug.Log("Ponto " + hit.point);

            hookPos = hit.point;

            float speed = Instantiate(hook, _camera.transform.position, _camera.transform.rotation).
            GetComponent<HookGrappler>().Speed;

            //if (hookPos == Vector3.zero||hit.transform.tag!="Terrain")
            if (hookPos==Vector3.zero)
            {
                activeHook = false;
                return;
            }

            Debug.DrawRay(_camera.transform.position, _camera.transform.forward * 20, Color.red, 5);

            MovePos = hit.point - (transform.position);

            startpos = hit.point;

            //activeHook = true;
            

            StartCoroutine(HookOn(MovePos.magnitude/speed));


            HookTimer = HookCooldown;
        }
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

    IEnumerator HookOn(float sec)
    {
        Debug.Log("fhgd");
        yield return new WaitForSeconds(sec);
        activeHook = true;
    }
}
