using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    [SerializeField] List<GameObject> ListParts;
    [SerializeField] List<BossWall> BossWalls;
    [SerializeField] float DmgPhaseTimer;
    [SerializeField] ChestScript Chest;
    [SerializeField] GameObject portal;

    public float BossHp;
    public bool isDead;
    public bool resetTurrets;
    float timer;


    public Image bossHp;
    public Image border;
    public bool activateUIHP;
    float saveMaxHp;

    private void Start()
    {
        border = GameObject.Find("Border").GetComponent<Image>();
        border.enabled = false;
        bossHp = GameObject.Find("bosshp").GetComponent<Image>();
        bossHp.enabled = false;
        saveMaxHp = BossHp;
    }
    void Update()
    {
        checkIfDead(BossHp);
        checkIfCanDmg();
        UIManager();
        //Debug.Log(isDead);
    }
    void UIManager()
    {
        if (activateUIHP)
        {
            border.enabled = true;
            bossHp.enabled = true;
        }
        else
        {
            border.enabled = false;
            bossHp.enabled = false;
        }
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
                else
                {
                    BossWalls[0].canIncrease = false;
                    BossWalls[1].canIncrease = false;
                    resetTurrets = false;
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
            activateUIHP = false;
            gameObject.SetActive(false);
            Chest.canAppear = true;
            portal.SetActive(true);
            
        }
        else
        {
            isDead = false;
            
        }
    }
    public void TakeDmg(float dmgToTake)
    {
        BossHp -= dmgToTake;
        bossHp.fillAmount = BossHp / saveMaxHp;
    }
  
}
