using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorsBehind : MonoBehaviour
{
    [SerializeField] GameObject Door;
    public bool canCloseDoor;
    public bool playSound;
    public float timer;
   [SerializeField] SurvivalScript surv;
    DoorScript_Survival survdoor;
    [SerializeField] DeffStageController deffcont;
    [SerializeField] AudioSource audio;

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
                playSound = true;
                if(audio != null) audio.Stop();
                canCloseDoor = false;
                //surv.SurvText.enabled = false;
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
