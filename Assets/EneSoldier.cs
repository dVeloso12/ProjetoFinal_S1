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


    private void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        base.Start();   
    }

    // Update is called once per frame
    void Update()
    {
        if (navagent.velocity.magnitude<1)
            animator.SetBool("IsMoving", false);
        else
            animator.SetBool("IsMoving", true);
        base.Update();
        shootingTimer += Time.deltaTime;

    }

    protected override void Action()
    {
        if (shootingTimer > 1f / fireRate)
        {
            bulletsParent.transform.LookAt(player);

            Instantiate(bulletPrefab, bulletsParent.transform.position, bulletsParent.transform.rotation, bulletsParent.transform);

            shootingTimer = 0f;
        }
    }
}
