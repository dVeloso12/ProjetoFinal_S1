using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretScript : MonoBehaviour
{
    //public fireProjectile weapon;
    public Transform target;
    public Transform barrel;
    public bool canAim;

    [Header("Weapon")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField]float RateOfFire;
    [SerializeField] GameObject pointOfBarrel;
    public bool firing;
    [SerializeField] float Damage = 3f;
    float fireTimer;
    [Header("General Stuff")]
    [SerializeField] float TurretHp;
    float saveMaxHp;
    [SerializeField] GameObject Boss;
    BossScript bosscp;
    public bool isAlive = true;
    public Detection detect;
    [SerializeField] Image HpBar;
    public bool showUI;
    Camera cam;
    public Vector3 ofseet;
    public bool isTutorial = false;

    AudioSource audio;

    private void Start()
    {
        bosscp = Boss.GetComponent<BossScript>();
        audio = GetComponent<AudioSource>();
        saveMaxHp = TurretHp;        
        HpBar.enabled = false;
        HpBar.fillAmount = 1f;
        cam = GameObject.Find("1stPersonCamera").GetComponent<Camera>();

    }
    private void Update()
    {
       
        if (canAim && isAlive)
        {
            if(!isTutorial)
            {
                target = detect.savePlayer;
                RotateTurret();
                Shoot();
            }
           
            UIManager();
        }
        if (bosscp.resetTurrets)
        {
            ResetTurret();
        }

    }

    void UIManager()
    {
        if (showUI)
        {
            HpBar.enabled = true;
        }
        else
        {
            HpBar.enabled = false;

        }

        if(HpBar.enabled)
        {
            HpBar.transform.rotation = Quaternion.LookRotation(HpBar.transform.position + ofseet - cam.transform.position);
        }
        
      
    }

    public void ResetTurret()
    {
        TurretHp = saveMaxHp;
        HpBar.fillAmount = 1f;
        showUI = true;
        isAlive = true;
    }

    public void TakeDmg(float dmg)
    {
        TurretHp -= dmg;
        HpBar.fillAmount = TurretHp / saveMaxHp;

        if(TurretHp <= 0)
        {
            showUI = false;
            isAlive = false;
        }
    }

    private void RotateTurret()
    {
        // TURN
        float angleY = vector3AngleOnPlane(target.position, transform.position, -transform.up, transform.forward);
        Vector3 rotationY = new Vector3(0, angleY, 0);
        transform.Rotate(rotationY, Space.Self);

        // UP / DOWN
        if (barrel != null)
        {
            //float angleX = vector3AngleOnPlane(target.position, transform.position, -transform.right, transform.forward);
            float angleX = vector3AngleOnPlane(target.position - ofseet , barrel.position, -transform.right, transform.forward);

            Vector3 rotationX = new Vector3(angleX, 0, 0);
            barrel.localRotation = Quaternion.Euler(rotationX);
        }
    }

        float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toZeroAngle)
        {
            Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
            float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toZeroAngle, planeNormal);

            return projectedVectorAngle;
        }
        
    void Shoot()
    {

        if(firing)
        {
            while (fireTimer >= 1f / RateOfFire)
            {
                audio.Play();
                Instantiate(bulletPrefab, pointOfBarrel.transform.position, pointOfBarrel.transform.rotation).GetComponent<BulletScript>().setDmg(Damage);
                fireTimer -= 1f / RateOfFire;
            }

            fireTimer += Time.deltaTime;
            firing = false;
        }
        else
        {
            if (fireTimer < 1f / RateOfFire)
            {
                fireTimer += Time.deltaTime;
            }
            else
            {
                fireTimer = 1f / RateOfFire;
            }
        }
      
       
    }

    public void Fire()
    {
        firing = true;
    }

}
