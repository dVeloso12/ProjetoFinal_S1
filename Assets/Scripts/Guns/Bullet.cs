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
        transform.position = Vector3.zero;
        StartPos = transform.position;
        
        Velocity = transform.forward;

        StartCoroutine(Des());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Velocity, Time.deltaTime);
        
        if((transform.position-StartPos).magnitude>MaxDistance)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {


        Destroy(gameObject);
    }

    IEnumerator Des()
    {

        Debug.Log(transform.position);
        yield return new WaitForSeconds(2);

        Debug.Log(transform.position);
        Destroy(gameObject);
    }
}
