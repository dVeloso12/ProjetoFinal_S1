using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject hitPrefab;
    public GameObject muzzlePrefab;
    public float speed;
    public float lifeTime;
    float timer;

    Rigidbody rb;
    Vector3 velocity;

    void Awake()
    {
        TryGetComponent(out rb);
    }

    void Start()
    {
        //var muzzleEffect = Instantiate(muzzlePrefab, transform.position, transform.rotation);
        //Destroy(muzzleEffect, 5f);
        velocity = transform.forward * speed;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        var displacement = velocity * Time.deltaTime;
        rb.MovePosition(rb.position + displacement);
        if(timer >= lifeTime)
        {
            Destroy(gameObject);
        }
        
    }

    //void OnCollisionEnter(Collision other)
    //{      
    //    if(other.collider)
    //    {
    //        //var hitEffect = Instantiate(hitPrefab, other.GetContact(0).point, Quaternion.identity);
    //        //Destroy(hitEffect, 5f);
    //        Destroy(gameObject);
    //    }
    //}
}
