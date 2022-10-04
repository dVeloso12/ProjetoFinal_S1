using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorsBehind : MonoBehaviour
{
    [SerializeField] GameObject Door;
    bool canCloseDoor;
    float timer;

    private void Update()
    {
        if(canCloseDoor)
        {
            timer += Time.deltaTime;
            if(timer > 3f)
            {
                Door.SetActive(true);
                timer = 0;
                canCloseDoor = false;
            }
            
          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
         
        if(other.transform.name == "Player")
        {
            canCloseDoor = true;
        }
    }
}
