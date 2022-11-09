using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI_2 : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    NavMeshAgent navagent;





    [Header("Shooting")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletsParent;

    [SerializeField] float fireRate = 60f;


    float shootingTimer;
    private void Awake()
    {
        navagent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }
    void Start()
    {
        shootingTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        shootingTimer += Time.deltaTime;
        navagent.SetDestination(player.position);

        if (navagent.remainingDistance<navagent.stoppingDistance+1)
            Shoot();
    }




    public void Shoot()
    {

        if (shootingTimer > 1f / fireRate)
        {
            Debug.Log("Shooting");

            bulletsParent.transform.LookAt(player);

            Instantiate(bulletPrefab, bulletsParent.transform.position, bulletsParent.transform.rotation, bulletsParent.transform);

            shootingTimer = 0f;
        }


    }
}
