using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    [SerializeField] GameObject Crystal;
    GameManager gm;
    CrystalScript ScpCrystal;
    bool startedPlate;
    [SerializeField] float IncreaseAmout; //0.3f
    float orIncrease;
    [SerializeField] public bool PlateCompleted;
    [SerializeField] ChestScript Chest;
    [SerializeField] StageSpawner spwaner;
    bool ReaperAttempt = true;
    [SerializeField] bool isTutorial = false;

    public float PlatePorc;
    int enterPlateTimes;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        PlatePorc = 0f;
        orIncrease = IncreaseAmout;
        ScpCrystal = Crystal.GetComponent<CrystalScript>();
        if (gm.DifficultyMod <= 3)
            IncreaseAmout = orIncrease / (gm.DifficultyMod);
        else
            IncreaseAmout = orIncrease / 3;
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
            //PlatePorc = 0f;
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
