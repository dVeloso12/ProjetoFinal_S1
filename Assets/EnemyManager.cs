using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update

    public float HP;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ETakeDmg(float damage)
    {
        HP -= damage;

        if (HP <= 0)
            Destroy(gameObject);
    }
}
