using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class AssaultRifle : GunController
{

    public float RandomDeviation, SniperMult;
    GameObject scope;
    [SerializeField] GameObject Gun;
    [SerializeField] TrailRenderer btrail;

    [SerializeField] Vector3 position;

    [HideInInspector]
    public int piercing = 3;

    [HideInInspector]
    public float SniperCD = 2;

    [SerializeField] AudioClip sniperSound, autoSound;


    void Start()
    {
        base.Start();
        playerInput.Player.Shoot.canceled += ActivateShoot;
        scope = GameObject.Find("Scope");
        scope.GetComponent<Image>().enabled = true;
        scope.SetActive(false);
        transform.localPosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        transform.localPosition = position;


    }


    public override void ActivateShoot(InputAction.CallbackContext obj)
    {

        //if (!isRunning)
        shoot = !shoot;
        //Debug.Log("Ãsd");
        //base.ActivateShoot(obj);


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

            weaponAudioSource.clip = autoSound;
            weaponAudioSource.Play();

            Vector3 ShootDir = _camera.forward;

            ShootDir.x += Random.Range(-RandomDeviation, RandomDeviation);
            ShootDir.y += Random.Range(-RandomDeviation, RandomDeviation);
            ShootDir.z += Random.Range(-RandomDeviation, RandomDeviation);


            //var tracer = Instantiate(NormalTrail, barrelMuzzle.transform.position, Quaternion.identity);
            //tracer.AddPosition(barrelMuzzle.transform.position);


            //Debug.Log(_camera.position + "  " + ShootDir + "" + _camera.forward);

            if (Physics.Raycast(_camera.position, ShootDir, out hit, Distance, layerMask))
            {
                Instantiate(MarkSprite, hit.point + (hit.normal * .1f),
                Quaternion.LookRotation(hit.normal)).transform.Rotate(Vector3.right * 90);
                //hiteffect.transform.position = hit.point;
                //hiteffect.transform.forward = hit.normal;
                //hiteffect.Emit(1);

                if (hit.transform.tag == "Enemy")
                {
                    finaldmg = dmg * gm.DamageMod;
                    hit.transform.GetComponent<EnemyStatus>().Damage(finaldmg);

                    GameObject dmgnum = Instantiate(dmgText, hit.point + (hit.normal * .1f),
                    Quaternion.LookRotation(hit.normal));
                    //dmgnum.transform.parent = hit.transform;
                    dmgnum.transform.Rotate(Vector3.up * 180);
                    dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, Color.white, hit.transform);
                }
                if (hit.transform.tag == "Head")
                {
                    finaldmg = dmg * gm.HSMod * gm.DamageMod;
                    hit.transform.GetComponentInParent<EnemyStatus>().Damage(finaldmg);

                    GameObject dmgnum = Instantiate(dmgText, hit.point + (hit.normal * .1f),
                    Quaternion.LookRotation(hit.normal));
                    //dmgnum.transform.parent = hit.transform;
                    dmgnum.transform.Rotate(Vector3.up * 180);
                    dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, Color.red, hit.transform);
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

                Debug.LogWarning("Shot : " + hit.transform.tag);

                if (playerBulletsScript == null)
                {
                    Debug.LogError("Player bullets script is null");
                    playerBulletsScript = Player.gameObject.GetComponent<PlayerBullets>();

                }

                if (playerBulletsScript != null && (hit.transform.tag == "Enemy" || hit.transform.tag == "Head" || hit.transform.tag == "Boss"))
                {

                    playerBulletsScript.StartBulletEffect(hit.transform.gameObject);

                }

                //Debug.DrawRay(_camera.position, ShootDir * hit.distance, Color.green);

            }

            base.Shoot();

        }
    }
    void ShootMod()
    {
        if (FireRateCounting <= 0 && Ammo > 0)
        {
            int i = 0, j = 0;
            Vector3 ShootDir = _camera.forward;

            weaponAudioSource.clip = sniperSound;
            weaponAudioSource.Play();


            if (Physics.Raycast(_camera.position, ShootDir, out base.hit, Distance * 2f))
            {

                hiteffect.transform.position = hit.point;
                hiteffect.transform.forward = hit.normal;
                hiteffect.Emit(1);

                TrailRenderer trail = Instantiate(btrail, _camera.position, Quaternion.identity);
                //Debug.Log(_camera.position + "  " + ShootDir + "" + _camera.forward + "" + hit.Length+""+ ShotingPlace.position+""+ hit[0].point);
                StartCoroutine(SpawnTrail(trail, hit.point));


                //foreach (var v in hit)
                //{
                //    Debug.Log(v.transform.tag+j);
                //    j++;
                //}

                //foreach(RaycastHit hit in hit) {

                //    Debug.Log("HeaadMUuuuuu"+i);

                if (hit.transform.tag == "Enemy")
                {
                    finaldmg = dmg * gm.DamageMod * SniperMult;
                    hit.transform.GetComponent<EnemyStatus>().Damage(finaldmg);


                    GameObject dmgnum = Instantiate(dmgText, hit.point + (hit.normal * .9f),
                    Quaternion.LookRotation(hit.normal));
                    //dmgnum.transform.parent = collisionDetected.transform;
                    dmgnum.transform.Rotate(Vector3.up * 180);
                    dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, Color.white, hit.transform);
                    i++;

                }

                if (hit.transform.tag == "Head")
                {
                    finaldmg = dmg * gm.HSMod * gm.DamageMod * SniperMult;
                    hit.transform.GetComponentInParent<EnemyStatus>().Damage(finaldmg);


                    GameObject dmgnum = Instantiate(dmgText, hit.point + (hit.normal * .1f),
                    Quaternion.LookRotation(hit.normal));
                    //dmgnum.transform.parent = hit.transform;
                    Debug.Log("HeaadMU");
                    dmgnum.transform.Rotate(Vector3.up * 180);
                    dmgnum.GetComponent<DmgTxt>().ChangeText((int)finaldmg, Color.red, hit.transform);
                    i++;

                }
                //Dano no Boss

                if (hit.transform.tag == "Boss")
                {
                    hit.transform.GetComponent<BossPart>().TakeDmgBoss(hit.transform.gameObject, base.dmg * gm.DamageMod);
                    i++;

                }
                if (hit.transform.tag == "Turret")
                {
                    hit.transform.GetComponent<TurretScript>().TakeDmg(base.dmg * gm.DamageMod);
                }


                //if (playerBulletsScript != null && (hit.transform.tag == "Enemy" || hit.transform.tag == "Head" || hit.transform.tag == "Boss"))
                //{

                //    playerBulletsScript.StartBulletEffect(hit.transform.gameObject);

                //}

                //Debug.Log(i);
                //if (i >= piercing)
                //    break;

            }


            base.Shoot();
            FireRateCounting += SniperCD;

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
        Debug.Log("ARMOD");
        if (gameObject.activeSelf)
        {
            if (Modifier)
            {
                StartCoroutine(ActivateScope());
            }
            else
                DeactivateScope();
        }
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

    IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hit)
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




}

