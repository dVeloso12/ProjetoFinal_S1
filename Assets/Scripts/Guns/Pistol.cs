using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
        if (Physics.Raycast(_camera.position, _camera.forward, out collisionDetected, Distance))
        {
            Instantiate(MarkSprite, collisionDetected.point + (collisionDetected.normal * .1f),
            Quaternion.LookRotation(collisionDetected.normal)).transform.Rotate(Vector3.right * 90);


            if (collisionDetected.transform.tag == "Enemy")
                collisionDetected.transform.GetComponent<EnemyManager>().ETakeDmg(dmg * gm.DamageMod);

        }

        shoot = !shoot;

        base.Shoot();
    }
}
