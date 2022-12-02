using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StageSpawner : MonoBehaviour
{
    [SerializeField] List<Encounters> PossibleEncounters;

    [SerializeField] List<GameObject> Enemies;

    
    public List<GameObject> EnemiesList;

    [SerializeField] float timertoSpwan;
    [SerializeField] int maxQuantity, enemiesToSpawnQuantity,distance;

    [SerializeField] bool ActivateSpawn = false;

    [SerializeField] string areaName;

    [SerializeField] Transform spawnpoint;

    GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();



    }

    // Update is called once per frame
    void Update()
    {
        if (ActivateSpawn)
        {
            SpawnEnemies();
            ActivateSpawn = false;
        }
    }

    void SpawnEnemies()
    {
        
        NavMeshHit hit;
        for(int i = 0; i < enemiesToSpawnQuantity; i++)
        {
            Vector2 randomcircle = (Random.insideUnitCircle * 30);
            Vector3 randomPoint = spawnpoint.position + new Vector3(randomcircle.x,0,randomcircle.y);
            bool sucess=false;
            int f = 600;
            while (!sucess&&f>0)
            {

                f--;
                if(f<1)
                    
                Debug.Log(f + "Attempt: " + randomPoint + "   " + NavMesh.GetAreaFromName(areaName));
                randomcircle = (Random.insideUnitCircle * distance);
                randomPoint = spawnpoint.position + new Vector3(randomcircle.x,0,randomcircle.y);
                if (NavMesh.SamplePosition(randomPoint, out hit, 10,-1))
                {
                   sucess = true;
                    
                   gm.EnemyList.Add(Instantiate(Enemies[0], hit.position, Quaternion.identity));
                }
               
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Spwan"+other.name+other.transform.position);
            SpawnEnemies();
            this.enabled=false;
        }
    }
}
