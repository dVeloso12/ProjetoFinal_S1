using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossPortal : MonoBehaviour
{
    [SerializeField] Vector3 Shop;

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.name == "Player")
        {
            other.gameObject.transform.position = Shop;
            Physics.SyncTransforms();
        }
    }

}
