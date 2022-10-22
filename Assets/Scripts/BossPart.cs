using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPart : MonoBehaviour
{

    [SerializeField] string PartBodyName;
    [SerializeField] GameObject Boss;
    [SerializeField] float MultiplierBoostDmg;


    public void TakeDmgBoss(GameObject check,float dmg)
    {
        
        if(check.name == PartBodyName)
        {
            //Debug.Log(check.name);
            Boss.GetComponent<BossScript>().TakeDmg(dmg * MultiplierBoostDmg);
            
        }
               
    }
}
