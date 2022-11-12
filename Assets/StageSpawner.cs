using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StageSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> Enemies;

    EnemyManager enemyManagerInstance;
    GameObject instantiatedPlayer;
    public List<GameObject> activeEnemiesList;

    [SerializeField] float timertoSpwan;
    [SerializeField] int maxQuantity, enemiesToSpawnQuantity,distance;

    [SerializeField] bool ActivateSpawn = false;

    [SerializeField] string areaName;

    [SerializeField] Transform spawnpoint;

    void Start()
    {
        enemyManagerInstance = EnemyManager.Instance;

        //instantiatedPlayer = enemyManagerInstance.instantiatedPlayer;
        activeEnemiesList = new List<GameObject>();



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
            int f = 60;
            while (!sucess&&f>0)
            {

                f--;
                Debug.Log(f + "Attempt: " + randomPoint + "   " + NavMesh.GetAreaFromName(areaName));
                randomcircle = (Random.insideUnitCircle * distance);
                randomPoint = spawnpoint.position + new Vector3(randomcircle.x,0,randomcircle.y);
                if (NavMesh.SamplePosition(randomPoint, out hit, 4,-1))
                {
                   sucess = true;
                   activeEnemiesList.Add(Instantiate(Enemies[0], hit.position, Quaternion.identity));
                }
               
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        SpawnEnemies();
    }
}
