using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunController
{
    public float RandomDeviation;

    public int Pellets;
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
        for (int i = 0; i < Pellets; i++)
        {
            Quaternion rotation = ShotingPlace.rotation;

            rotation.x += Random.Range(-RandomDeviation, RandomDeviation);
            rotation.y += Random.Range(-RandomDeviation, RandomDeviation);
            rotation.z += Random.Range(-RandomDeviation, RandomDeviation);
            rotation.w += Random.Range(-RandomDeviation, RandomDeviation);

            Instantiate(bullet, ShotingPlace.position, rotation);
        }

        FireRateCounting = FireRate;

        shoot = !shoot;
    }
}
