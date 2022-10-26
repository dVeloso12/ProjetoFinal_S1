using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] List<GameObject> ListParts;

    public float BossHp;
    public bool isDead;
    

    void Update()
    {
        checkIfDead(BossHp);
        //Debug.Log(isDead);
    }

    void checkIfDead(float hp)
    {
        if(hp <= 0f)
        {
            isDead = true;
        }else
        {
            isDead = false;
        }
    }
    public void TakeDmg(float dmgToTake)
    {
        BossHp -= dmgToTake;
    }
  
}
