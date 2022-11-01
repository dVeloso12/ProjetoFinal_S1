using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{

    public static EnemyManager Instance;

    private List<GameObject> activeEnemiesList;

    public GameObject instantiatedPlayer;
    public SurvivalScript survivalScript;

    public GameObject InstantiatedPlayer
    {
        get { return instantiatedPlayer; }
    }
    public List<GameObject> ActiveEnemiesList
    {
        get { return activeEnemiesList; }
    }

    public EnemyManager(GameObject playerObject, GameObject survivalMap)
    {

        instantiatedPlayer = playerObject;

        Instance = this;
        activeEnemiesList = new List<GameObject>();

        survivalScript = survivalMap.GetComponentInChildren<SurvivalScript>();

        if (survivalMap == null) Debug.Log("survival e nulo");


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
