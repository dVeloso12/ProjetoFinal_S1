using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float Speed;

    public float Damage;

    public float MaxDistance;

    private Vector3 StartPos;

   Vector3 Velocity;


    void Start()
    {
        
        StartPos = transform.position;
        
        Velocity = transform.forward;
        Debug.Log("NEW BULLET");
        StartCoroutine(Des());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Velocity,Speed* Time.deltaTime);
        
        if((transform.position-StartPos).magnitude>MaxDistance)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("adfskhjjasdsdhvasdsliu"+collision.transform.name);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<Player>().Damage(Damage);

        Destroy(gameObject);
    }

    IEnumerator Des()
    {

        //Debug.Log(transform.position);
        yield return new WaitForSeconds(5);

        //Debug.Log(transform.position);
        Destroy(gameObject);
    }
}
