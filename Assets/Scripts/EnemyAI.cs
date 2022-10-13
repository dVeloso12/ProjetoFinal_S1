using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] GameObject PlayerObject;


    [SerializeField] float gunRange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(transform.position, PlayerObject.transform.position) < gunRange)
        {

            if (Aim())
                Shoot();
            else
                FollowPlayer();

        }
        else
        {

            FollowPlayer();

        }
    }
    


    public bool Aim()
    {

        RaycastHit hitInfo;
        Vector3 target = PlayerObject.transform.position;

        transform.LookAt(target);

        if(Physics.Raycast(transform.position, transform.forward, out hitInfo))
        {

            return true;
        }
        else
        {
            return false;
        }

    }

    public void Shoot()
    {

        //Shooting
        Debug.Log("Shooting");
    }

    public void FollowPlayer()
    {



    }

}
