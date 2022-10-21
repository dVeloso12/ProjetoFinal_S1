using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlateBoss : MonoBehaviour
{

    [SerializeField] GameObject Plate;
    [SerializeField] GameObject Crystal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Plate.GetComponent<PlateBossRoom>().canIncreaseBeam = true;
     
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Plate.GetComponent<PlateBossRoom>().canIncreaseBeam = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            Plate.GetComponent<PlateBossRoom>().IncreaseLoadPlate();
            if(Plate.GetComponent<PlateBossRoom>().plateCompleted == false) Crystal.GetComponent<SpinCrystal>().Spin();
        }
    }
}
