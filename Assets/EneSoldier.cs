using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneSoldier : Enemy_AI_2
{

    [Header("Shooting")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletsParent;
    [SerializeField] float fireRate = 60f;
    float shootingTimer=0;


    private void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        base.Start();   
    }

    // Update is called once per frame
    void Update()
    {
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
