using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDeffScript : MonoBehaviour
{
    [SerializeField] GameObject Plate;
    [SerializeField] GameObject Door;
    [SerializeField] ChestScript Chest;
    PlateScript plateScrp;
    bool doorOpened;
    //GameManager gm;
    void Start()
    {
        plateScrp = Plate.GetComponent<PlateScript>();
        //gm = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if(plateScrp.PlateCompleted && !doorOpened)
        {
            Door.SetActive(false);
            doorOpened = true;
            Chest.canAppear = true;
            //gm.AddUpgrade();

        }
    }
}
