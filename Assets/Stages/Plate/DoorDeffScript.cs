using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDeffScript : MonoBehaviour
{
    [SerializeField] GameObject Plate;
    [SerializeField] GameObject Door;
    [SerializeField] Vector3 maxPos;

    PlateScript plateScrp;
    void Start()
    {
        plateScrp = Plate.GetComponent<PlateScript>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(plateScrp.PlateCompleted)
        {
           
                Door.transform.position += new Vector3(Door.transform.position.x,
                Door.transform.position.y + 0.01f * Time.deltaTime, Door.transform.position.z);
            
         
        }
    }
}
