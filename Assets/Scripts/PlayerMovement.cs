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
    bool jump = false;
    bool groundedPlayer;
    float gravityValue = -15;


    public Transform cameraTransform;

    Vector3 movement,playerVelocity;

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

        playerInput.Player.Jump.performed += Jump;
        playerInput.Player.Run.performed += Run;
        playerInput.Player.Run.canceled += Run;
    }
    // Update is called once per frame

    private void Update()
    {
        Vector3 camRotation = cameraTransform.eulerAngles;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, camRotation.y, transform.eulerAngles.z);
    }
    void FixedUpdate()
    {
        if (jump)
        {

            if (groundedPlayer)
            {
                playerVelocity.y = Mathf.Sqrt(jumpForce * -3.0f * -10);
            }
            jump = false;
        }

        Move();

     
    }

    void Movement()
    {
        CheckIsGrounded();

        Vector2 _movement = playerInput.Player.Movement.ReadValue<Vector2>();

        Debug.Log("X : " + _movement.x + "; Y : ;" +  _movement.y);


        

        movement = transform.forward * _movement.y + transform.right * _movement.x;

        if (IsGrounded && movement.y < 0)
        {
            movement.y = 0f;
        }



        movement *= playerSpeed;

        Debug.Log("Movement: " + movement);

        movement.y += -10*Time.deltaTime;

        float step = playerSpeed * Time.fixedDeltaTime;

        //transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, step);

        controller.Move(movement*Time.deltaTime);

        //playerVelocity.y += gravityValue * Time.deltaTime;

    }

    private void Jump(InputAction.CallbackContext obj)
    {

        jump = true;

        Debug.Log("Jump");

    }

    void CheckIsGrounded()
    {
        
        float radius = 0.5f;

        Collider[] groundColliders = Physics.OverlapSphere(GroundCheck.transform.position, radius, groundLayerMask);


        //Debug.Log(groundColliders.Length);
        
        //foreach(Collider collider in groundColliders)
        //{
        //    Debug.Log("Collider : " + collider.name);
        //}

        if (groundColliders.Length > 0)
        {
            IsGrounded = true;
        }
        else
            IsGrounded = false;
        
        



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


        Vector3 camRotation = cameraTransform.eulerAngles;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, camRotation.y, transform.eulerAngles.z);

        Vector2 playerMovement = playerInput.Player.Movement.ReadValue<Vector2>();
        Vector3 move = new Vector3(playerMovement.x, 0, playerMovement.y);

        //move.z = w+s; move.x = a+d; forward = para onde estamos a olhar; right = movimento dos lados
        move = transform.forward * move.z + transform.right * move.x;
        move.y = 0f;

        move.Normalize(); //para evitar movimento mais r�pido na diagonal

       controller.Move(move * Time.deltaTime * playerSpeed*gm.MoveSpeedMod);

       playerVelocity.y += gravityValue * Time.deltaTime;

       controller.Move(playerVelocity * Time.deltaTime);




    }

}
