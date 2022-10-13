using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssaultRifle : GunController
{

    public float RandomDeviation;
    void Start()
    {
        base.Start();
        playerInput.Player.Shoot.canceled += ActivateShoot;

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    
    public override void ActivateShoot(InputAction.CallbackContext obj)
    {

            shoot = !shoot;
            Debug.Log("Ãsd");


    }
    protected override void Shoot()
    {
        if (FireRateCounting <= 0)
        {

            Quaternion rotation = ShotingPlace.rotation;

            rotation.x += Random.Range(-RandomDeviation, RandomDeviation);
            rotation.y += Random.Range(-RandomDeviation, RandomDeviation);
            rotation.z += Random.Range(-RandomDeviation, RandomDeviation);
            rotation.w += Random.Range(-RandomDeviation, RandomDeviation);

            Instantiate(bullet, ShotingPlace.position, rotation);



            FireRateCounting = FireRate;
        }
    }
}
