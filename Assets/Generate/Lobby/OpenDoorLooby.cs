using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorLooby : MonoBehaviour
{
    [SerializeField] Animation door;
    bool doorOpen;
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if(other.gameObject.name == "Player")
        {
            audio.Play();
                door.Play("doorOpen");       
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            audio.Play();
            door.Play("doorClose");

        }
    }
}
