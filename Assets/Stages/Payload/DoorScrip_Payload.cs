using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScrip_Payload : MonoBehaviour
{
    [SerializeField] Payload payload;
    [SerializeField] ChestScript chest;
    bool doorOpened;

    void Update()
    {
        if(payload.Ended && !doorOpened)
        {
            this.gameObject.SetActive(false);
            doorOpened = true;
            chest.canAppear = true;


        }
    }
}
