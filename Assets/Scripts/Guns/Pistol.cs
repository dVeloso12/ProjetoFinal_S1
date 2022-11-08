using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Pistol : GunController
{

    [SerializeField] int CritRate=10;
    [SerializeField] float CritDmg=1.5f;
    
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

            int r = UnityEngine.Random.Range(0, 100);



            if (collisionDetected.transform.tag == "Enemy")
            {
                finaldmg = dmg * gm.DamageMod;
                if (r < CritRate)
                    finaldmg *= CritDmg;
                collisionDetected.transform.GetComponent<EnemyStatus>().Damage(finaldmg);


                Color color = Color.white;

                GameObject dmgnum = Instantiate(dmgText, collisionDetected.point + (collisionDetected.normal * .1f),
                Quaternion.LookRotation(collisionDetected.normal));
                dmgnum.transform.parent = collisionDetected.transform;
                dmgnum.transform.Rotate(Vector3.up * 180);
                if (r < CritRate)
                    color = Color.yellow;
                dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, color);
            }

            if (collisionDetected.transform.tag == "Head")
            {
                finaldmg = dmg * gm.HSMod * gm.DamageMod;
                if (r < CritRate)
                    finaldmg *= CritDmg;
                collisionDetected.transform.GetComponentInParent<EnemyStatus>().Damage(finaldmg);


                Color color = Color.red;

                GameObject dmgnum = Instantiate(dmgText, collisionDetected.point + (collisionDetected.normal * .1f),
                Quaternion.LookRotation(collisionDetected.normal));
                dmgnum.transform.parent = collisionDetected.transform;
                dmgnum.transform.Rotate(Vector3.up * 180);
                if (r < CritRate)
                    color = Color.cyan;
                dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, color);
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

        shoot = !shoot;

        base.Shoot();
    }
}
