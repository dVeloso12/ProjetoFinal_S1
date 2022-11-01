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
        if (FireRateCounting <= 0&& Ammo>0)
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
                {
                    //collisionDetected.transform.GetComponent<EnemyManager>().ETakeDmg(dmg * gm.DamageMod);
                    collisionDetected.transform.GetComponent<EnemyStatus>().Damage(dmg * gm.DamageMod);
                //    Instantiate(dmgText,collisionDetected.point + (collisionDetected.normal * .1f),
                //Quaternion.LookRotation(collisionDetected.normal)).transform.Rotate(Vector3.right * 90);
                }
                if (collisionDetected.transform.tag == "Head")
                {
                    //collisionDetected.transform.GetComponent<EnemyManager>().ETakeDmg(dmg * gm.DamageMod);
                    collisionDetected.transform.GetComponentInParent<EnemyStatus>().Damage(dmg*gm.HSMod* gm.DamageMod);
                }
                //Dano no Boss
                if (collisionDetected.transform.tag == "Boss")
                {
                    collisionDetected.transform.GetComponent<BossPart>().TakeDmgBoss(collisionDetected.transform.gameObject, base.dmg * gm.DamageMod);
                }
                if (collisionDetected.transform.tag == "Turret")
                {
                    collisionDetected.transform.GetComponent<TurretScript>().TakeDmg(base.dmg * gm.DamageMod);
                }

            }



            base.Shoot();
        }
    }
}
