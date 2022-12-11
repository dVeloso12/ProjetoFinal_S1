using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : GunController
{

    [SerializeField]public int CritRate=10;
    [SerializeField]public float CritDmg=1.5f;

    [SerializeField] Vector3 position;

    Animator pistolAnimator;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        transform.localPosition = position;
        pistolAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        transform.localPosition = position;
    }

    protected override void Shoot()
    {
        if (Physics.Raycast(_camera.position, _camera.forward, out hit, Distance))
        {
            Instantiate(MarkSprite, hit.point + (hit.normal * .1f),
            Quaternion.LookRotation(hit.normal), hit.transform).transform.Rotate(Vector3.right * 90);

            int r = UnityEngine.Random.Range(0, 100);



            if (hit.transform.tag == "Enemy")
            {
                finaldmg = dmg * gm.DamageMod;
                if (r < CritRate)
                    finaldmg *= CritDmg;
                hit.transform.GetComponent<EnemyStatus>().Damage(finaldmg);


                Color color = Color.white;

                GameObject dmgnum = Instantiate(dmgText, hit.point + (hit.normal * .1f),
                Quaternion.LookRotation(hit.normal));
                //dmgnum.transform.parent = hit.transform;
                dmgnum.transform.Rotate(Vector3.up * 180);
                if (r < CritRate)
                    color = Color.yellow;
                dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, color,hit.transform);
            }

            if (hit.transform.tag == "Head")
            {
                finaldmg = dmg * gm.HSMod * gm.DamageMod;
                if (r < CritRate)
                    finaldmg *= CritDmg;
                hit.transform.GetComponentInParent<EnemyStatus>().Damage(finaldmg);


                Color color = Color.red;

                GameObject dmgnum = Instantiate(dmgText, hit.point + (hit.normal * .1f),
                Quaternion.LookRotation(hit.normal));
                //dmgnum.transform.parent = hit.transform;
                dmgnum.transform.Rotate(Vector3.up * 180);
                if (r < CritRate)
                    color = Color.cyan;
                dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, color,hit.transform);
            }

            //Dano no Boss
            if (hit.transform.tag == "Boss")
            {
                hit.transform.GetComponent<BossPart>().TakeDmgBoss(hit.transform.gameObject, base.dmg * gm.DamageMod);
            }
            if (hit.transform.tag == "Turret")
            {
                hit.transform.GetComponent<TurretScript>().TakeDmg(base.dmg * gm.DamageMod);
            }


        }

        shoot = !shoot;

        base.Shoot();
    }

   
}
