using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Granade : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Granada;

    PlayerInput playerInput;

    public Transform place;

    void Start()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        playerInput.Player.Granade.performed += CreateNade;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateNade(InputAction.CallbackContext context)
    {
        Instantiate(Granada,place.position,place.rotation);
    }

}
