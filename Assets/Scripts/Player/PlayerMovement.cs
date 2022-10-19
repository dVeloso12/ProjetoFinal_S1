using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float runningModifier;

    [SerializeField] GameObject GroundCheck;
    [SerializeField] LayerMask groundLayerMask;

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
    float TDashCooldown;
    public float DashTime;
    float VDashTime;
    Vector3 DashDir;


    public Transform cameraTransform;

    Vector3 move,playerVelocity;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        gm = FindObjectOfType<GameManager>();

        IsGrounded = false;
        IsRunning = false;
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
            TDashCooldown -= Time.deltaTime;
     
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
        }
        else
        {
            IsRunning = true;
            playerSpeed *= runningModifier;
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
        }


        //Vector3 camRotation = cameraTransform.eulerAngles;
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, camRotation.y, transform.eulerAngles.z);

        Vector2 playerMovement = playerInput.Player.Movement.ReadValue<Vector2>();
        move = new Vector3(playerMovement.x, 0, playerMovement.y);

        //move.z = w+s; move.x = a+d; forward = para onde estamos a olhar; right = movimento dos lados
        move = transform.forward * move.z + transform.right * move.x;
        move.y = 0f;

        move.Normalize(); //para evitar movimento mais r�pido na diagonal

        if (DashActive)
        {
            if (VDashTime > 0)
            {
                controller.Move(DashDir * DashSpeed * Time.deltaTime);
                VDashTime -= Time.deltaTime;
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
            DashDir = move;
            DashDir.y = 0;
            TDashCooldown = DashCooldown;
        }
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
