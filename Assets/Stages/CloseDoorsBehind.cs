using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorsBehind : MonoBehaviour
{
    [SerializeField] GameObject Door;
    bool canCloseDoor;
    float timer;

    Pathfinder pathfinder;
    GenerateRun generator;


    private void Start()
    {
        pathfinder = Pathfinder.Instance;
        generator = GenerateRun.instance;
    }

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
            generator.PassedDoor();
            canCloseDoor = true;
        }
    }
}
