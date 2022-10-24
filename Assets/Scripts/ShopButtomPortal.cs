using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtomPortal : MonoBehaviour
{
    [SerializeField] GameObject Portal;
    public bool playerColide;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == "Player" && Input.GetKeyUp(KeyCode.H))
        {
            Portal.SetActive(true);
            playerColide = true;
            
        }
        else if(other.transform.name == "Player")
        {
            playerColide = false;
        }

    }
}
