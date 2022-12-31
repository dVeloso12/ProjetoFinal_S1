using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float runningModifier;

    [SerializeField] GameObject GroundCheck;
    [SerializeField] LayerMask groundLayerMask;

    [SerializeField] GameObject GunDirectionOBJ;

    PlayerInput playerInput;

    public CharacterController controller;

    bool IsGrounded;
    bool IsRunning;

    //Jump Stuff
    bool jump = false;
    bool groundedPlayer;
    float gravityValue = -15;
    int JumpAmount = 1;
    public float jumpTimer=0;


    //Dash Stuff

    public float DashSpeed;
    bool DashActive;
    public float DashCooldown;
    float TDashCooldown=0;
    public float DashTime;
    float VDashTime;
    Vector3 DashDir;
    public Image dashIcon;
    public float fillAmount;
    public AudioSource footspets,dashsound;
    bool moving,inair;

    public Transform cameraTransform;

    Vector3 move,playerVelocity;

    GameManager gm;

    //TextMeshProUGUI Timer;

    Animator gunAnimator;

    public CinemachineVirtualCamera _cinemachine;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        gm = FindObjectOfType<GameManager>();

        IsGrounded = false;
        IsRunning = false;

        //Timer = GameObject.Find("DashC").GetComponent<TextMeshProUGUI>();
        dashIcon = GameObject.Find("DashImage").GetComponent<Image>();

        gunAnimator = GunDirectionOBJ.GetComponentInChildren<Animator>();
        footspets.pitch = 1.3f + gm.MoveSpeedMod * .1f;
        footspets.Play();
        footspets.Pause();

    }

    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerInput.Player.Jump.performed += CheckJump;
        playerInput.Player.Run.performed += Run;
        playerInput.Player.Run.canceled += Run;
        playerInput.Player.Dash.performed += ActivateDash;

        VDashTime = DashTime;
    }
    // Update is called once per frame

    private void Update()
    {
        Vector3 camRotation = cameraTransform.eulerAngles;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, camRotation.y, transform.eulerAngles.z);
    }
    void FixedUpdate()
    {
        if (groundedPlayer)
            JumpAmount = 1;
        if (jump)
        {

                Jump();
            jump = false;
        }

        Move();


        if (TDashCooldown > 0)
        {
            TDashCooldown -= Time.deltaTime;
            fillAmount += Time.deltaTime * (1 / (DashCooldown/gm.CDMod));
            //Timer.text = Mathf.Round(TDashCooldown).ToString();
            dashIcon.fillAmount = fillAmount;
        }
    }

    private void CheckJump(InputAction.CallbackContext obj)
    {
        jump = true;
    }


    void Jump()
    {
        if (JumpAmount > 0 && jumpTimer <= 0)
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -3.0f * gravityValue);
            //if (!isonGround)
            JumpAmount--;
            //jumptimer = 0.4f;
        }
        jumpTimer -= Time.deltaTime;
    }

   
    private void Run(InputAction.CallbackContext context)
    {

        if (IsRunning)
        {
            IsRunning = false;
            playerSpeed /= runningModifier;
            footspets.pitch = 1.1f + gm.MoveSpeedMod * .2f;

        }
        else
        {
            IsRunning = true;
            playerSpeed *= runningModifier;
            footspets.pitch = 1.3f + gm.MoveSpeedMod * .2f;

        }



        if (gunAnimator == null) gunAnimator = GunDirectionOBJ.GetComponentInChildren<Animator>();

        if(gunAnimator != null)
        {
            gunAnimator.SetBool("IsRunning", IsRunning);
            GunController tempScript = gunAnimator.gameObject.GetComponent<GunController>();

            tempScript.IsRunning = this.IsRunning;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(GroundCheck.transform.position, 0.5f);
    }

    private void Move()
    {
        groundedPlayer = controller.isGrounded;


        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            //StartCoroutine(StopSteps());
            inair = false;
        }
        else inair = true;


        //Vector3 camRotation = cameraTransform.eulerAngles;
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, camRotation.y, transform.eulerAngles.z);

        Vector2 playerMovement = playerInput.Player.Movement.ReadValue<Vector2>();
        move = new Vector3(playerMovement.x, 0, playerMovement.y);

        //move.z = w+s; move.x = a+d; forward = para onde estamos a olhar; right = movimento dos lados
        move = transform.forward * move.z + transform.right * move.x;
        move.y = 0f;

        move.Normalize(); //para evitar movimento mais r�pido na diagonal

        if(moving && move.magnitude < .1f||inair)
        {
            
            moving = false;

            footspets.Pause();
        }
        if(!moving && move.magnitude > .1f&&!inair)
        {
            moving = true;
            footspets.UnPause();
            
        }

        

        if (DashActive)
        {
            if (VDashTime > 0)
            {
                controller.Move(DashDir * DashSpeed * Time.deltaTime);
                VDashTime -= Time.deltaTime;
                dashsound.Play();
            }
            else
            {
                VDashTime = DashTime;

                DashActive = false;
            }
        }
        else
        {
            controller.Move(move * Time.deltaTime * playerSpeed * gm.MoveSpeedMod);

            playerVelocity.y += gravityValue * Time.deltaTime;

            controller.Move(playerVelocity * Time.deltaTime);

        }


    }


    void ActivateDash(InputAction.CallbackContext context)
    {
        if (TDashCooldown <= 0)
        {
            DashActive = true;
            if (move != Vector3.zero)
                DashDir = move;
            else
                DashDir = transform.forward;
            DashDir.y = 0;
            TDashCooldown = DashCooldown/gm.CDMod;
            fillAmount = 0;
        }
    }


    public void ChangeCameraHSense(float hori)
    {
        _cinemachine.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = hori*2;
        
    }

    public void ChangeCameraVSense(float verti)
    {
        
        _cinemachine.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = verti * 2;
    }

     IEnumerator StopSteps()
    {
        yield return new WaitForSeconds(.5f);

        if (groundedPlayer || playerVelocity.y < 0)
        {
            
            inair = false;
        }
        else inair = true;

    }





}





//Funcoes Mortas





//void Movement()
//{
//    CheckIsGrounded();

//    Vector2 _movement = playerInput.Player.Movement.ReadValue<Vector2>();

//    Debug.Log("X : " + _movement.x + "; Y : ;" + _movement.y);




//    movement = transform.forward * _movement.y + transform.right * _movement.x;

//    if (IsGrounded && movement.y < 0)
//    {
//        movement.y = 0f;
//    }



//    movement *= playerSpeed;

//    Debug.Log("Movement: " + movement);

//    movement.y += -10 * Time.deltaTime;

//    float step = playerSpeed * Time.fixedDeltaTime;

//    //transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, step);

//    controller.Move(movement * Time.deltaTime);

//    //playerVelocity.y += gravityValue * Time.deltaTime;

//}


//void CheckIsGrounded()
//{

//    float radius = 0.5f;

//    Collider[] groundColliders = Physics.OverlapSphere(GroundCheck.transform.position, radius, groundLayerMask);




//    if (groundColliders.Length > 0)
//    {
//        IsGrounded = true;
//    }
//    else
//        IsGrounded = false;





//}
