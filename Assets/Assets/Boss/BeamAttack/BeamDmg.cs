using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDmg : MonoBehaviour
{
    public float dmg = 1f;

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("DealingDMGBeam");
            other.GetComponent<Player>().Damage(dmg);
        }
    }
}
