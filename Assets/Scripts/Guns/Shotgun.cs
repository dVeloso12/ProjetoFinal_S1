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
            Ammo = 1;
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

            if (Physics.Raycast(_camera.position, ShootDir, out hit, Distance))
            {
                Instantiate(MarkSprite, hit.point + (hit.normal * .1f),
                Quaternion.LookRotation(hit.normal),hit.transform).transform.Rotate(Vector3.right * 90);
                hiteffect.transform.position = hit.point;
                hiteffect.transform.forward = hit.normal;
                hiteffect.Emit(1);

                if (hit.transform.tag == "Enemy")
                {


                    finaldmg = dmg * gm.DamageMod;
                   
                    GameObject dmgnum = Instantiate(dmgText, hit.point + (hit.normal * .1f),
                   Quaternion.LookRotation(hit.normal));
                    //dmgnum.transform.parent = hit.transform;
                    dmgnum.transform.Rotate(Vector3.up * 180);
                    dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, Color.white,hit.transform);
                    textDmg += finaldmg;
                    hit.transform.GetComponent<EnemyStatus>().Damage(finaldmg);

                    body++;

                }

                if (hit.transform.tag == "Head")
                {
                    finaldmg = dmg * gm.HSMod * gm.DamageMod;
                    if (textPos == Vector3.zero)
                        textPos = hit.point;
                    hit.transform.GetComponentInParent<EnemyStatus>().Damage(finaldmg);
                    GameObject dmgnum = Instantiate(dmgText, hit.point + (hit.normal * .1f),
                    Quaternion.LookRotation(hit.normal));
                    //dmgnum.transform.parent = hit.transform;
                    dmgnum.transform.Rotate(Vector3.up * 180);
                    dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, Color.red,hit.transform);

                    textDmg += finaldmg;
                    head++;
                }

                    //Dano no Boss
                    if (hit.transform.tag == "Boss")
                {

                    hit.transform.GetComponent<BossPart>().TakeDmgBoss(hit.transform.gameObject,base.dmg * gm.DamageMod); 
                }
                //Debug.Log(collisionDetected.transform.tag);
                if (hit.transform.tag == "Turret")
                {
                    hit.transform.GetComponent<TurretScript>().TakeDmg(base.dmg * gm.DamageMod);
                }

                

            }
        }


        shoot = !shoot;

        base.Shoot();
    }
}
