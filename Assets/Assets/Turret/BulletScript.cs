using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    float timer;
    public float dmg;

    Rigidbody rb;
    Vector3 velocity;

    void Awake()
    {
        TryGetComponent(out rb);
    }

    void Start()
    {
        velocity = transform.forward * speed;
    }
    public void setDmg(float newDmg)
    {
        dmg = newDmg;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        var displacement = velocity * Time.deltaTime;
        rb.MovePosition(rb.position + displacement);
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") other.GetComponent<Player>().Damage(dmg);
     
    }
}
