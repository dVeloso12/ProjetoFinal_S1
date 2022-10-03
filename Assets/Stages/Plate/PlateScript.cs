using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    [SerializeField] GameObject Crystal;
    CrystalScript ScpCrystal;
    bool startedPlate;
    [SerializeField] float Speed;
    [SerializeField] float IncreaseAmout; //0.3f
    [SerializeField] public bool PlateCompleted;

    public float PlatePorc;
    int enterPlateTimes;
    void Start()
    {
        ScpCrystal = Crystal.GetComponent<CrystalScript>();
    }

    void Update()
    {
        if(startedPlate)
        {
            ScpCrystal.StartAnim = true;
        }
        if(PlatePorc >= 1f)
        {
            PlateCompleted = true;
            PlatePorc = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(enterPlateTimes == 0)
        {
            startedPlate = true;
            enterPlateTimes++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == "Player")
        {
            if (!PlateCompleted)
            {
                PlatePorc += IncreaseAmout * Speed * Time.deltaTime;
                ScpCrystal.FillPorc = PlatePorc;
            }
      
        }
    }
   
}
