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
            Vector3 ShootDir = _camera.forward;

            ShootDir.x += Random.Range(-RandomDeviation, RandomDeviation);
            ShootDir.y += Random.Range(-RandomDeviation, RandomDeviation);
            ShootDir.z += Random.Range(-RandomDeviation, RandomDeviation);


            if (Physics.Raycast(_camera.position, ShootDir, out collisionDetected, Distance))
            {
                Instantiate(MarkSprite, collisionDetected.point + (collisionDetected.normal * .1f),
                Quaternion.LookRotation(collisionDetected.normal)).transform.Rotate(Vector3.right * 90);


                if (collisionDetected.transform.tag == "Enemy")
                    collisionDetected.transform.GetComponent<EnemyManager>().ETakeDmg(dmg * gm.DamageMod);

                //Dano no Boss
                if(collisionDetected.transform.tag == "Boss")
                {
                    collisionDetected.transform.GetComponent<BossPart>().TakeDmgBoss(collisionDetected.transform.gameObject,base.dmg * gm.DamageMod); 
                }
               
            }
        }


        shoot = !shoot;

        base.Shoot();
    }
}
