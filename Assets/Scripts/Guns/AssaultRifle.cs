using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AssaultRifle : GunController
{

    public float RandomDeviation,SniperMult;
    GameObject scope;
    [SerializeField] GameObject Gun;

    [SerializeField]TrailRenderer btrail;

    [SerializeField] Vector3 position;

    Animator ARAnimator;

    void Start()
    {
        base.Start();
        playerInput.Player.Shoot.canceled += ActivateShoot;
        scope = GameObject.Find("Scope");
        scope.GetComponent<Image>().enabled=true;
        scope.SetActive(false);
        transform.localPosition = position;

        ARAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        transform.localPosition = position;
    }

    
    public override void ActivateShoot(InputAction.CallbackContext obj)
    {

            shoot = !shoot;
            //Debug.Log("Ãsd");


    }
    protected override void Shoot()
    {
        if (Modifier)
        ShootMod();
        else ShootBase();


    }

    void ShootBase()
    {
        if (FireRateCounting <= 0 && Ammo > 0)
        {
            Vector3 ShootDir = _camera.forward;

            ShootDir.x += Random.Range(-RandomDeviation, RandomDeviation);
            ShootDir.y += Random.Range(-RandomDeviation, RandomDeviation);
            ShootDir.z += Random.Range(-RandomDeviation, RandomDeviation);



            if (Physics.Raycast(_camera.position, ShootDir, out hit, Distance))
            {
                Instantiate(MarkSprite, hit.point + (hit.normal * .1f),
                Quaternion.LookRotation(hit.normal)).transform.Rotate(Vector3.right * 90);
                hiteffect.transform.position = hit.point;
                hiteffect.transform.forward = hit.normal;
                hiteffect.Emit(1);

                if (hit.transform.tag == "Enemy")
                {
                    finaldmg = dmg * gm.DamageMod;
                    hit.transform.GetComponent<EnemyStatus>().Damage(finaldmg);

                    GameObject dmgnum = Instantiate(dmgText, hit.point + (hit.normal * .1f),
                    Quaternion.LookRotation(hit.normal));
                    //dmgnum.transform.parent = hit.transform;
                    dmgnum.transform.Rotate(Vector3.up * 180);
                    dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, Color.white,hit.transform);
                }
                if (hit.transform.tag == "Head")
                {
                    finaldmg = dmg * gm.HSMod * gm.DamageMod;
                    hit.transform.GetComponentInParent<EnemyStatus>().Damage(finaldmg);

                    GameObject dmgnum = Instantiate(dmgText, hit.point + (hit.normal * .1f),
                    Quaternion.LookRotation(hit.normal));
                    //dmgnum.transform.parent = hit.transform;
                    dmgnum.transform.Rotate(Vector3.up * 180);
                    dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, Color.red,hit.transform);
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
            base.Shoot();

        }
    }
    void ShootMod()
    {
        if(FireRateCounting <= -1 && Ammo > 0)
        {
            Vector3 ShootDir = _camera.forward;

            if (Physics.Raycast(_camera.position, ShootDir, out hit, Distance))
            {
                Instantiate(MarkSprite, hit.point + (hit.normal * .1f),
                Quaternion.LookRotation(hit.normal),hit.transform).transform.Rotate(Vector3.right * 90);
                hiteffect.transform.position = hit.point;
                hiteffect.transform.forward = hit.normal;
                hiteffect.Emit(1);

                TrailRenderer trail = Instantiate(btrail, ShotingPlace.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit.point));

                if (hit.transform.tag == "Enemy")
                {
                    finaldmg = dmg * gm.DamageMod * SniperMult;
                    hit.transform.GetComponent<EnemyStatus>().Damage(finaldmg);


                    GameObject dmgnum = Instantiate(dmgText, hit.point + (hit.normal * .1f),
                    Quaternion.LookRotation(hit.normal));
                    //dmgnum.transform.parent = collisionDetected.transform;
                    dmgnum.transform.Rotate(Vector3.up * 180);
                    dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, Color.white,hit.transform);
                }

                if (hit.transform.tag == "Head")
                {
                    finaldmg = dmg * gm.HSMod * gm.DamageMod * SniperMult;
                    hit.transform.GetComponentInParent<EnemyStatus>().Damage(finaldmg);


                    GameObject dmgnum = Instantiate(dmgText, hit.point + (hit.normal * .1f),
                    Quaternion.LookRotation(hit.normal));
                    //dmgnum.transform.parent = hit.transform;
                    dmgnum.transform.Rotate(Vector3.up * 180);
                    dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, Color.red,hit.transform);
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
            //else
            //{
            //    TrailRenderer trail = Instantiate(btrail, ShotingPlace.position, Quaternion.identity);
            //    StartCoroutine(SpawnTrail(trail, ShootDir));
            //}

            base.Shoot();

        }



    }

    public override void AimDown() 
    {

        if (Modifier)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, DwonSights.localPosition, 20 * Time.deltaTime);
            //        SetFOV(Mathf.Lerp(Camera.m_Lens.FieldOfView, .3f * 60, 3 * Time.deltaTime));
            //        scope.SetActive(true);
            //        Gun.SetActive(false);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 20 * Time.deltaTime);
            //        SetFOV(Mathf.Lerp(Camera.m_Lens.FieldOfView, 60, 3 * Time.deltaTime));
            //        scope.SetActive(false);
            //        Gun.SetActive(true);
        }

        //base.AimDown();
    }

    public override void _Modifier(InputAction.CallbackContext obj)
    {
        base._Modifier(obj);
        if (Modifier)
        {
            StartCoroutine(ActivateScope());
        }
        else
            DeactivateScope();
    }

    IEnumerator ActivateScope()
    {
        yield return new WaitForSeconds(.2f);
        if (Modifier)
        {
            SetFOV(.3f * 60);
            scope.SetActive(true);
            Gun.SetActive(false);
        }
    }
    void DeactivateScope()
    {
        SetFOV(60);
        scope.SetActive(false);
        Gun.SetActive(true);
    }

    IEnumerator SpawnTrail(TrailRenderer trail,Vector3 hit)
    {
        float time = 0;
        Vector3 startpos = trail.transform.position;
        //while (time < 1)
        //{
        //    trail.transform.position = Vector3.Lerp(startpos, hit.point, time);
        //    time += Time.deltaTime / .1f;
            yield return null;

        //}
        trail.transform.position = hit;

        Destroy(trail.gameObject, trail.time);
    }

    public override void ActivateReload(InputAction.CallbackContext obj)
    {
        ARAnimator.SetTrigger("Reload");
    }
        
    public void Reloaded()
    {
        Debug.Log("Reloading");
        Ammo = 0;
        Ammo = AmmoClipSize;

        Debug.Log("Ammo : " + Ammo);

        AmmoCount.text = Ammo.ToString() + "/" + AmmoClipSize.ToString();
    }

    

}

