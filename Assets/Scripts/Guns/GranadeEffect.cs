using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    public float throwForce;
    public float dmg;
    public GameObject Explosion;
    public int size;
    public AudioSource explosionSound;

    bool Exploded = false;
    void Start()
    {
        Rigidbody rb=gameObject.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward*throwForce, ForceMode.Impulse);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0&&!Exploded)
            Explode();
        timer -= Time.deltaTime;
       
    }

    void Explode()
    {
        Exploded = true;
        Collider[] collsions = Physics.OverlapSphere(transform.position, size);
        Debug.Log("asd" + collsions.Length);

        for (int i = 0; i < collsions.Length; i++)
        {


            if (collsions[i].gameObject.CompareTag("Enemy") == true)
            {
                //collsions[i].GetComponent<EnemyManager>().ETakeDmg(dmg);
                collsions[i].GetComponent<EnemyStatus>().Damage(dmg);
                Debug.Log("boom");

            }
            //Dano no Boss
            if (collsions[i].transform.tag == "Boss")
            {
                collsions[i].transform.GetComponent<BossPart>().TakeDmgBoss(collsions[i].transform.gameObject, dmg);
            }
            if (collsions[i].transform.tag == "Turret")
            {
                collsions[i].transform.GetComponent<TurretScript>().TakeDmg(dmg);
            }

        }
        Instantiate(Explosion, transform.position,Quaternion.identity);

        explosionSound.Play();
        StartCoroutine(BigPop());

    }


    IEnumerator BigPop()
    {
        
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);



    }
}
