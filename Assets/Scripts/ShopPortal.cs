using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPortal : MonoBehaviour
{
    public bool playerColide;

    private void OnTriggerEnter(Collider other)
    {

        if(other.transform.name == "Player")
        {
            Debug.Log(other.transform.name);
            playerColide = true;
            //gameObject.SetActive(false);
        }
        
    }
}
