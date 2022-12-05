using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseByScript : MonoBehaviour
{
    [SerializeField] GameObject Payload;

    private void OnTriggerStay(Collider other)
    {

        if (other.transform.name == "Player" && Payload.transform.position.x <= Payload.GetComponent<PayloadScript>().EndPoint)
        {        
                Payload.GetComponent<PayloadScript>().canMove = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.name == "Player")
        {
            Payload.GetComponent<PayloadScript>().canMove = false;

        }
    }
}
