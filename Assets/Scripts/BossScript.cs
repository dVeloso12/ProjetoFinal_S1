using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BossState
{
    Iddle,Shooting
}
public enum AttackType
{
    NormalAttack,BeamAttack,onBeamAttack
}
public class BossScript : MonoBehaviour
{

    [SerializeField] List<GameObject> ListParts;
    [SerializeField] List<BossWall> BossWalls;
    [SerializeField] float DmgPhaseTimer;
    [SerializeField] ChestScript Chest;
    [SerializeField] GameObject portal;
     Animator animator;

    public float BossHp;
    public bool isDead;
    public bool resetTurrets;
    public float timer;

    public BossState state;
    public AttackType type;

    public Image bossHp;
    public Image border;
    public bool activateUIHP;
    float saveMaxHp;
    public float offset;

    GameObject Player;

    bool firing;
    float fireTimer;
    float RateOfFire = 1f;
    public float BossNormalAttackDmg;
    public GameObject bulletPrefab;
    public GameObject pointOfBarrel;
    public GameObject Weapon;
    public RedHollowControl beam;
    [SerializeField] float maxNormalAttackTimer = 4f;
    [HideInInspector]
    public bool DamagePhase;
    float attackTimer;
    public bool isTutorial = false;

    private void Start()
    {
        border = GameObject.Find("Border").GetComponent<Image>();
        border.enabled = false;
        bossHp = GameObject.Find("bosshp").GetComponent<Image>();
        bossHp.enabled = false;
        animator = GetComponent<Animator>();
        saveMaxHp = BossHp;
        bossHp.fillAmount = 1f;
        state = global::BossState.Iddle;
        ChooseAttack();
        
    }
    void Update()
    {
        UpdateBossPosition();
        checkIfDead(BossHp);
        checkIfCanDmg();
        UIManager();
        BossState();
        AttackLogic();

        RaycastHit hit;
        int layerMask = 1 << 6;

        if (Physics.Raycast(pointOfBarrel.transform.position, pointOfBarrel.transform.forward, out hit, Mathf.Infinity,layerMask))
        {
            Debug.DrawRay(pointOfBarrel.transform.position, pointOfBarrel.transform.forward * 100f, Color.green);
        }
        else
        {
            Debug.DrawRay(pointOfBarrel.transform.position, pointOfBarrel.transform.forward * 100f, Color.red);
        }
    }

    void UpdateBossPosition()
    {
        if (Player != null)
        {
            float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toZeroAngle)
            {
                Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
                float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toZeroAngle, planeNormal);

                return projectedVectorAngle;
            }
            if(!beam.isAttacking)
            {
                float angleY = vector3AngleOnPlane(transform.position, Player.transform.position, -transform.up, -transform.forward);
                Vector3 rotationY = new Vector3(0, angleY - offset, 0);
                transform.Rotate(rotationY, Space.Self);


                float angleYW = vector3AngleOnPlane(Weapon.transform.position, Player.transform.position, -Weapon.transform.up, -Weapon.transform.right);
                Vector3 rotationYW = new Vector3(0, angleYW, 0);
                Weapon.transform.Rotate(rotationYW, Space.Self);

            }
 
        }
    }
    void BossState()
    {
        if(!isDead)
        {
            if (BossWalls[0].platesDones == true && BossWalls[1].platesDones == true)
            {
                state = global::BossState.Shooting;

                
                offset = 0f;
            }
            else
            {
                state = global::BossState.Iddle;
                offset = 40f;
            }
        }

        if (!isTutorial)
        {
            if (state == global::BossState.Iddle)
            {
                animator.SetTrigger("toIddle");

            }
            else if (state == global::BossState.Shooting)
            {
                if (type == AttackType.NormalAttack)
                {
                    attackTimer += Time.deltaTime;
                    animator.SetTrigger("toShoot");
                    firing = true;
                    if (attackTimer >= maxNormalAttackTimer)
                    {
                        ChooseAttack();
                        attackTimer = 0f;
                    }
                }
                if (type == AttackType.BeamAttack)
                {
                    beam.canAttack = true;
                    type = AttackType.onBeamAttack;

                }
                if (type == AttackType.onBeamAttack)
                {
                    if (!beam.canAttack)
                    {
                        ChooseAttack();
                    }
                }
            }
        }
        
    }

     void ChooseAttack()
    {
        if (timer <= DmgPhaseTimer)
        {
            int value = Random.Range(0, 2);
            switch (value)
            {
                case 0:
                    {
                        type = AttackType.NormalAttack;
                        break;
                    }
                case 1:
                    {
                        type = AttackType.BeamAttack;
                        break;
                    }
            }
        }
        else
        {
            type = AttackType.NormalAttack;
        }
    }

    void AttackLogic()
    {
        if(type == AttackType.NormalAttack)
        {
                if (firing)
                {
                    while (fireTimer >= 1f / RateOfFire)
                    {
                        Instantiate(bulletPrefab, pointOfBarrel.transform.position, pointOfBarrel.transform.rotation).GetComponent<BulletScript>().setDmg(BossNormalAttackDmg);
             
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
    }
    void UIManager()
    {
        if (activateUIHP)
        {
            border.enabled = true;
            bossHp.enabled = true;
        }
        else
        {
            border.enabled = false;
            bossHp.enabled = false;
        }
    }
    
 
    void checkIfCanDmg()
    {
        if (BossWalls[0].WallUp == false && BossWalls[1].WallUp == false)
        {
            DamagePhase = true;
            timer += Time.deltaTime;
            if (timer >= DmgPhaseTimer && type != AttackType.onBeamAttack)
            {
                state = global::BossState.Iddle;
                DamagePhase = false;
                if (!isDead)
                {
                    BossWalls[0].canIncrease = true;
                    BossWalls[1].canIncrease = true;
                    resetTurrets = true;
                    
                }
                else
                {
                    BossWalls[0].canIncrease = false;
                    BossWalls[1].canIncrease = false;
                    resetTurrets = false;
                }
               
            }
        }
        else
        {
            timer = 0f;
            resetTurrets=false;
        }
    }

    void checkIfDead(float hp)
    {
        if(hp <= 0f)
        {
            isDead = true;
            animator.SetTrigger("canDie");
            activateUIHP = false;
            gameObject.SetActive(false);
            Chest.canAppear = true;
            portal.SetActive(true);
            
        }
        else
        {
            isDead = false;
            
        }
    }
    public void TakeDmg(float dmgToTake)
    {
        BossHp -= dmgToTake;
        bossHp.fillAmount = BossHp / saveMaxHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Player = other.gameObject;
        }
    }
}
