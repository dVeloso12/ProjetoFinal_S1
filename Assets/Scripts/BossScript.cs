using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] List<GameObject> ListParts;
    [SerializeField] GameObject BossWall;
    [SerializeField] float DmgPhaseTimer;
    BossWall wall;
    public float BossHp;
    public bool isDead;
    public bool resetTurrets;
    float timer;
    private void Start()
    {
        wall = BossWall.GetComponent<BossWall>();
    }

    void Update()
    {
        checkIfDead(BossHp);
        checkIfCanDmg();
        //Debug.Log(isDead);
    }

    void checkIfCanDmg()
    {
        if(wall.WallUp == false)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= DmgPhaseTimer)
            {
                if (!isDead)
                {
                    wall.canIncrease = true;
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
