using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{

    public static EnemyManager Instance;

    private List<GameObject> activeEnemiesList;


    public List<GameObject> ActiveEnemiesList
    {
        get { return activeEnemiesList; }
    }

    public EnemyManager()
    {

        Instance = this;
        activeEnemiesList = new List<GameObject>();

    }


    public void ActivateEnemy(GameObject enemyToActivate)
    {
        activeEnemiesList.Add(enemyToActivate);
    }

    public void DeActivateEnemy(GameObject enemytoDeActivate)
    {
        activeEnemiesList.Remove(enemytoDeActivate);
    }



}
