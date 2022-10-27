using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] List<GameObject> ListParts;
    [SerializeField] List<BossWall> BossWalls;
    [SerializeField] float DmgPhaseTimer;
    public float BossHp;
    public bool isDead;
    public bool resetTurrets;
    float timer;
    void Update()
    {
        checkIfDead(BossHp);
        checkIfCanDmg();
        //Debug.Log(isDead);
    }

    void checkIfCanDmg()
    {
        if (BossWalls[0].WallUp == false && BossWalls[1].WallUp == false)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= DmgPhaseTimer)
            {
                if (!isDead)
                {
                    BossWalls[0].canIncrease = true;
                    BossWalls[1].canIncrease = true;
                    resetTurrets = true;
                    
                }
               
            }
        }
        else
        {
            timer = 0f;
            resetTurrets=false;
        }
    }

    void checkIfDead(float hp)
    {
        if(hp <= 0f)
        {
            isDead = true;
            gameObject.SetActive(false);
        }
        else
        {
            isDead = false;
            
        }
    }
    public void TakeDmg(float dmgToTake)
    {
        BossHp -= dmgToTake;
    }
  
}
