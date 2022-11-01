using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "Player")
        {
            other.GetComponent<Player>().isdead = true;
        }
    }
}
