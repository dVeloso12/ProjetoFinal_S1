using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorsBehind : MonoBehaviour
{
    [SerializeField] GameObject Door;
    public bool canCloseDoor;
    public float timer;
   [SerializeField] SurvivalScript surv;
    DoorScript_Survival survdoor;
    [SerializeField] DeffStageController deffcont;

    private void Start()
    {
        survdoor = Door.GetComponent<DoorScript_Survival>();
    }


    private void Update()
    {
        if(canCloseDoor)
        {
            timer += Time.deltaTime;
            if(timer > 3f)
            {
                if(survdoor != null) survdoor.Hide_ShowDoor(true);
                else
                {
                    Door.SetActive(true);
                }

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
            if(survdoor != null) survdoor.PlayerPass(true);
            if (deffcont != null) deffcont.disableObjTxt();

        }
    }
}
