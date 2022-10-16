using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{

    public GameObject bullet;

    public Transform ShotingPlace;

    protected PlayerInput playerInput;

    protected bool shoot=false;

    public float FireRate;

    protected float FireRateCounting;

    protected GameManager gm;

    // Start is called before the first frame update
    protected void Start()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        playerInput.Player.Shoot.performed += ActivateShoot;

        FireRateCounting = FireRate;

        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    protected  virtual void Update()
    {
        if (shoot)
            Shoot();

        FireRateCounting -= Time.deltaTime;
    }

    public virtual void ActivateShoot(InputAction.CallbackContext obj)
    {

        if(FireRateCounting<=0)
            shoot = !shoot;


    }

    protected virtual void Shoot()
    {
        // Instantiate(bullet, ShotingPlace.position, ShotingPlace.rotation);

        FireRateCounting = FireRate*gm.FireRateMod;

    }
}
