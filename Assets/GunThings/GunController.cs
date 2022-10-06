using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{

    public GameObject bullet;

    public Transform ShotingPlace;

    PlayerInput playerInput;

    bool shoot=false;

    public float FireRate;

    float FireRateCounting;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        playerInput.Player.Shoot.performed += ActivateShoot;
        playerInput.Player.Shoot.canceled += ActivateShoot;

        FireRateCounting = FireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot && FireRateCounting<=0)
            Shoot();

        FireRateCounting -= Time.deltaTime;
    }

    public void ActivateShoot(InputAction.CallbackContext obj)
    {

            shoot = !shoot;


    }

    void Shoot()
    {
        Instantiate(bullet, ShotingPlace.position, ShotingPlace.rotation);
        FireRateCounting = FireRate;

    }
}
