using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorLooby : MonoBehaviour
{
    [SerializeField] Animation door;
    bool doorOpen;

    private void OnTriggerEnter(Collider other)
    {
     
        if(other.gameObject.name == "Player")
        {
                door.Play("doorOpen");       
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            door.Play("doorClose");

        }
    }
}
