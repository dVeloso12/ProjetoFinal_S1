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
        int qauntity = Pellets;
        if (Modifier)
        {
            qauntity *= Ammo;
            Ammo = 0;
        }

        for (int i = 0; i < qauntity; i++)
        {
            Vector3 ShootDir = _camera.forward;

            ShootDir.x += Random.Range(-RandomDeviation, RandomDeviation);
            ShootDir.y += Random.Range(-RandomDeviation, RandomDeviation);
            ShootDir.z += Random.Range(-RandomDeviation, RandomDeviation);

            Vector3 textPos=Vector3.zero;
            float textDmg = 0;
            int head = 0, body = 0;

            if (Physics.Raycast(_camera.position, ShootDir, out collisionDetected, Distance))
            {
                Instantiate(MarkSprite, collisionDetected.point + (collisionDetected.normal * .1f),
                Quaternion.LookRotation(collisionDetected.normal)).transform.Rotate(Vector3.right * 90);
                hiteffect.transform.position = collisionDetected.point;
                hiteffect.transform.forward = collisionDetected.normal;
                hiteffect.Emit(1);

                if (collisionDetected.transform.tag == "Enemy")
                {


                    finaldmg = dmg * gm.DamageMod;
                   
                    GameObject dmgnum = Instantiate(dmgText, collisionDetected.point + (collisionDetected.normal * .1f),
                   Quaternion.LookRotation(collisionDetected.normal));
                    dmgnum.transform.parent = collisionDetected.transform;
                    dmgnum.transform.Rotate(Vector3.up * 180);
                    dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, Color.white);
                    textDmg += finaldmg;
                    collisionDetected.transform.GetComponent<EnemyStatus>().Damage(finaldmg);

                    body++;

                }

                if (collisionDetected.transform.tag == "Head")
                {
                    finaldmg = dmg * gm.HSMod * gm.DamageMod;
                    if (textPos == Vector3.zero)
                        textPos = collisionDetected.point;
                    collisionDetected.transform.GetComponentInParent<EnemyStatus>().Damage(finaldmg);
                    GameObject dmgnum = Instantiate(dmgText, collisionDetected.point + (collisionDetected.normal * .1f),
                    Quaternion.LookRotation(collisionDetected.normal));
                    dmgnum.transform.parent = collisionDetected.transform;
                    dmgnum.transform.Rotate(Vector3.up * 180);
                    dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, Color.red);

                    textDmg += finaldmg;
                    head++;
                }

                    //Dano no Boss
                    if (collisionDetected.transform.tag == "Boss")
                {

                    collisionDetected.transform.GetComponent<BossPart>().TakeDmgBoss(collisionDetected.transform.gameObject,base.dmg * gm.DamageMod); 
                }
                //Debug.Log(collisionDetected.transform.tag);
                if (collisionDetected.transform.tag == "Turret")
                {
                    collisionDetected.transform.GetComponent<TurretScript>().TakeDmg(base.dmg * gm.DamageMod);
                }

                

            }
        }


        shoot = !shoot;

        base.Shoot();
    }
}
