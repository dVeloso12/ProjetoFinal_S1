using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookGrappler : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startPos;

    public float Reach;

    Vector3 Velocity;

    public GameObject player;

    public float Speed;

    void Start()
    {
        startPos = transform.position;

        Velocity = transform.forward * 3;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Velocity, Speed * Time.deltaTime);
    

        if((startPos-transform.position).magnitude>Reach)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("aSD");


        //gameObject.GetComponentInParent<HookShot>().ActivateHook(transform.position);


        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aSD");


        //gameObject.GetComponentInParent<HookShot>().ActivateHook(transform.position);


        //Destroy(gameObject);
    }
}
