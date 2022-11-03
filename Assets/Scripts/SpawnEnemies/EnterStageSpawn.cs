using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterStageSpawn : MonoBehaviour
{

    [SerializeField] int quantityTospawn;
    [SerializeField] int quantityToSpawnOverTime;

    [SerializeField] SurvivalScript survivalScript;

    Spawn spawnScript;



    private void Start()
    {
        spawnScript = GenerateRun.instance.EnemiesManagerInstantiated.GetComponent<Spawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Colliding with : " + other.name);

        if(other.tag == "Player")
        {

            if(transform.parent.gameObject.name == "Stage3_Survive")
            {
                survivalScript.EnemiesKilled = 0;
            }

            //Debug.Log("Colliding with player 01");
            spawnScript.SpawnEnemiesWithQuantity(quantityTospawn, quantityToSpawnOverTime);
            Collider collider = this.GetComponent<Collider>();
            collider.enabled = false;

        }
    }
}
