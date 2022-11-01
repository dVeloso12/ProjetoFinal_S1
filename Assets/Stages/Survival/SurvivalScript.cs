using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalScript : MonoBehaviour
{
    [SerializeField] DoorScript_Survival survivalDoor;
    [SerializeField] int nObjective;
    int zombiesKilled;

    public bool isSurvivalCompleted;

    // Start is called before the first frame update
    void Start()
    {
        zombiesKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSurvivalCompleted)
            survivalDoor.CompleteSurvival();
    }

    public void ZombieKilled()
    {
        zombiesKilled++;

        if(zombiesKilled == nObjective)
        {
            survivalDoor.CompleteSurvival();
        }
    }


}
