using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterStageSpawn : MonoBehaviour
{

    [SerializeField] int quantityTospawn;
    [SerializeField] int quantityToSpawnOverTime;

    [SerializeField] Spawn spawnScript;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding with : " + other.name);

        if(other.tag == "Player")
        {
            Debug.Log("Colliding with player 01");
            spawnScript.SpawnEnemies(quantityTospawn, quantityToSpawnOverTime);
            Collider collider = this.GetComponent<Collider>();
            collider.enabled = false;

        }
    }
}
