using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneExploder : Enemy_AI_2
{

    public GameObject Explosion;
    public int ExplosionSize, VisualSize;


    void Awake()
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
    }

    protected override void Action()
    {

        GameObject expl= Instantiate(Explosion, transform.position, Quaternion.identity);
        expl.transform.localScale = new Vector3(VisualSize, VisualSize, VisualSize);



        Collider[] collsions = Physics.OverlapSphere(transform.position, ExplosionSize);
        Debug.Log("asd" + collsions.Length);

        for (int i = 0; i < collsions.Length; i++)
        {
            if (collsions[i].gameObject.CompareTag("Player") == true)
            {
                collsions[i].GetComponent<Player>().Damage(dmg);
                Debug.Log("boomEnemy");

            }
        }

        GetComponent<EnemyStatus>().Damage(1000000);
    }

}
