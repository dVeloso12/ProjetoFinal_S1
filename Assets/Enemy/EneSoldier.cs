using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneSoldier : Enemy_AI_2
{

    [Header("Shooting")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletsParent;
    [SerializeField] float fireRate = 60f;
    Animator animator;
    float shootingTimer=0;

    [SerializeField] bool isSniper = false;

    LineRenderer line;

    private void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        line = GetComponentInChildren<LineRenderer>();
    }
    void Start()
    {
        base.Start();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!Frozen) {
            if (navagent.velocity.magnitude < 1)
            {
                animator.SetBool("IsMoving", false);
                if(isSniper)
                    line.enabled = true;

            }
            else
            {
                animator.SetBool("IsMoving", true);
                if (isSniper)
                    line.enabled = false;

            }


            base.Update();
            shootingTimer += Time.deltaTime;

            if (shootingTimer > 1f / fireRate)
            {                
                animator.SetBool("CanShoot", true);
            } 
        }
    }

    protected override void Action()
    {
            bulletsParent.transform.LookAt(player);

            Instantiate(bulletPrefab, bulletsParent.transform.position, bulletsParent.transform.rotation, bulletsParent.transform)
            .GetComponent<Bullet>().Damage=dmg;

            shootingTimer = 0f;
        animator.SetBool("CanShoot", false);
    }
}
