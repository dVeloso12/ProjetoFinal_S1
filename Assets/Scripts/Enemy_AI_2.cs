using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI_2 : MonoBehaviour
{
    // Start is called before the first frame update
    protected Transform player;
    NavMeshAgent navagent;


    //[Header("Shooting")]
    [SerializeField] int DistanceToPlayer;


    
    [SerializeField] protected int dmg;
    [SerializeField] float speed;


    protected void Awake()
    {
        navagent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>().transform;
        navagent.speed = speed;
        navagent.stoppingDistance = DistanceToPlayer;
    }
    protected void Start()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        Debug.Log("Update");
       
        navagent.SetDestination(player.position);

        Vector3 directionToLook = (player.position - transform.position);
        float angleDiference = Vector3.Angle(directionToLook.normalized, transform.forward);

        if (angleDiference > .5f || angleDiference < -.5f)
        {
            transform.LookAt(player);
        }

        float DistanceToPlayer = (transform.position - player.position).magnitude;

        if (DistanceToPlayer < navagent.stoppingDistance + 1)
        {
            Debug.Log(DistanceToPlayer + " " + (navagent.stoppingDistance + 1));

            Action();
        }

    }

    protected virtual void Action()
    {

    }


    public void Shoot()
    {

        


    }
}
