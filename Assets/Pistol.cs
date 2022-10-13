using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : GunController
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void Shoot()
    {
        Instantiate(bullet, ShotingPlace.position, ShotingPlace.rotation);

        FireRateCounting = FireRate;

        shoot = !shoot;
    }
}
