using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{

    public static EnemyManager Instance;

    private List<GameObject> activeEnemiesList;

    private GameObject instantiatedPlayer;


    public GameObject InstantiatedPlayer
    {
        get { return instantiatedPlayer; }
    }
    public List<GameObject> ActiveEnemiesList
    {
        get { return activeEnemiesList; }
    }

    public EnemyManager(GameObject playerObject)
    {

        instantiatedPlayer = playerObject;

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
