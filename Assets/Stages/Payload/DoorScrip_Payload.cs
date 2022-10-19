using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScrip_Payload : MonoBehaviour
{
    [SerializeField] GameObject payload;

    bool doorOpened;

    GameManager gm;


    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(payload.GetComponent<PayloadScript>().PayloadCompleted && !doorOpened)
        {
            this.gameObject.SetActive(false);
            doorOpened = true;

            gm.AddUpgrade();
        }
    }
}
