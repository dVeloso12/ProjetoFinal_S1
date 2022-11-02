using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterStageSpawn : MonoBehaviour
{

    [SerializeField] int quantityTospawn;
    [SerializeField] int quantityToSpawnOverTime;

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
            //Debug.Log("Colliding with player 01");
            spawnScript.SpawnEnemiesWithQuantity(quantityTospawn, quantityToSpawnOverTime);
            Collider collider = this.GetComponent<Collider>();
            collider.enabled = false;

        }
    }
}
