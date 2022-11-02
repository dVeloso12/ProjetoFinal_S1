using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalScript : MonoBehaviour
{
    [SerializeField] DoorScript_Survival survivalDoor;
    [SerializeField] int nObjective;
    int enemiesKilled;

    public bool isSurvivalCompleted;


    public int EnemiesKilled
    {
        get { return enemiesKilled; }
        set { enemiesKilled = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSurvivalCompleted)
            survivalDoor.CompleteSurvival();
    }

    public void ZombieKilled()
    {
        enemiesKilled++;

        Debug.Log("Enemies killed : " + enemiesKilled);

        if(enemiesKilled == nObjective)
        {
            survivalDoor.CompleteSurvival();
        }
    }


}
