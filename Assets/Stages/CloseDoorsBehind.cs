using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorsBehind : MonoBehaviour
{
    [SerializeField] GameObject Door;
    bool canCloseDoor;

    private void Update()
    {
        if(canCloseDoor)
        {
            Door.SetActive(true);
            //porta nao aparece 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
         
        if(other.transform.name == "Player")
        {
            Debug.Log(other.transform.name);
            canCloseDoor = true;
        }
    }
}
