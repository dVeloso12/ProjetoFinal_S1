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
    Rigidbody playerRB;

    bool IsGrounded;
    bool IsRunning;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        IsGrounded = false;
        IsRunning = false;
    }

    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        playerInput.Player.Jump.performed += Jump;
        playerInput.Player.Run.performed += Run;
        playerInput.Player.Run.canceled += Run;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        Movement();

    }

    void Movement()
    {

        Vector2 _movement = playerInput.Player.Movement.ReadValue<Vector2>();

        Debug.Log("X : " + _movement.x + "; Y : ;" +  _movement.y);




        Vector3 movement = transform.forward * _movement.y + transform.right * _movement.x;
        
        movement *= playerSpeed * Time.fixedDeltaTime;

        Debug.Log("Movement: " + movement);

        float step = playerSpeed * Time.fixedDeltaTime;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, step);

    }

    private void Jump(InputAction.CallbackContext obj)
    {

        CheckIsGrounded();

        if (IsGrounded)
        {
            float finalJumpForce = playerRB.mass * 5f + jumpForce;
            playerRB.AddForce(transform.up * finalJumpForce, ForceMode.Impulse);
        }

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

}
