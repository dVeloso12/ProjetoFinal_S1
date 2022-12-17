using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorsBehind : MonoBehaviour
{
    [SerializeField] GameObject Door;
    public bool canCloseDoor;
    public float timer;
   [SerializeField] SurvivalScript surv;


    private void Update()
    {
        if(canCloseDoor)
        {
            timer += Time.deltaTime;
            if(timer > 3f)
            {
                Door.GetComponent<DoorScript_Survival>().Hide_ShowDoor(true);
                timer = 0;
                canCloseDoor = false;
                surv.SurvText.enabled = false;
            }
            
          
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.transform.name);
        if(other.transform.name == "Player")
        {
            //generator.PassedDoor();
            canCloseDoor = true;
            Door.GetComponent<DoorScript_Survival>().PlayerPass(true);

        }
    }
}
