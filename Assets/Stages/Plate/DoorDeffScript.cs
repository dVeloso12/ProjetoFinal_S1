using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDeffScript : MonoBehaviour
{
    [SerializeField] GameObject Plate;
    [SerializeField] GameObject Door;
    PlateScript plateScrp;
    bool doorOpened;
    void Start()
    {
        plateScrp = Plate.GetComponent<PlateScript>();   
    }
    void Update()
    {
        if(plateScrp.PlateCompleted && !doorOpened)
        {
            Door.SetActive(false);
            doorOpened = true;
            
        }
    }
}
