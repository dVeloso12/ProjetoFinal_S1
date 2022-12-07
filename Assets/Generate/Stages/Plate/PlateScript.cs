using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    [SerializeField] GameObject Crystal;
    CrystalScript ScpCrystal;
    bool startedPlate;
    [SerializeField] float IncreaseAmout; //0.3f
    [SerializeField] public bool PlateCompleted;
    [SerializeField] ChestScript Chest;
    [SerializeField] StageSpawner spwaner;
    bool ReaperAttempt = true;
    [SerializeField] bool isTutorial = false;

    public float PlatePorc;
    int enterPlateTimes;
    void Start()
    {
        ScpCrystal = Crystal.GetComponent<CrystalScript>();
    }

    void Update()
    {
        if (PlatePorc>=.8f && ReaperAttempt && !isTutorial)
        {
            spwaner.ReaperSpawn();
            ReaperAttempt = false;
        }
        if (startedPlate)
        {
            ScpCrystal.StartAnim = true;
        }
        if(PlatePorc >= 1f)
        {
            PlateCompleted = true;
            PlatePorc = 0f;
        }
        if(PlateCompleted)
        {
            Chest.canAppear = true;
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
                PlatePorc += IncreaseAmout * 0.005f * Time.deltaTime;
                ScpCrystal.FillPorc = PlatePorc;
            }
      
        }
    }
   
}
