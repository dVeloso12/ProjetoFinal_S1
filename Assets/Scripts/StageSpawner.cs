using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StageSpawner : MonoBehaviour
{
    [SerializeField] List<Encounters> PossibleEncounters;
    Encounters Encounter;

    [SerializeField] GameObject REAPER;

    
    public List<GameObject> EnemiesList;

    [SerializeField] float timertoSpwan;
    [SerializeField] int maxQuantity, enemiesToSpawnQuantity,distance;

    [SerializeField] bool ActivateSpawn = false;

    [SerializeField] string areaName;

    [SerializeField] Transform spawnpoint;

    int Current_SemiEncounter=0;
    float Timer_SemiEncounter=1;
    bool activated = false;

    GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();



    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (Timer_SemiEncounter <= 0)
            {
                SpawnEnemies();

            }
            Timer_SemiEncounter -= Time.deltaTime;
        }
    }

    void SpawnEnemies()
    {
        
        NavMeshHit hit;

        SemiEncounter sEncounter = Encounter.SemiEncounters[Current_SemiEncounter];

        if (gm.EnemyList.Count < 15)
        {

            for (int eSet = 0; eSet < sEncounter.EnemySet.Count; eSet++)
            {

                for (int i = 0; i < sEncounter.Quantity[eSet]; i++)
                {
                    Vector2 randomcircle = (Random.insideUnitCircle * sEncounter.Area[eSet]);
                    Vector3 randomPoint = sEncounter.Position[eSet]+transform.position + new Vector3(randomcircle.x, 0, randomcircle.y);
                    bool sucess = false;
                    int f = 600;
                    while (!sucess && f > 0)
                    {
                        f--;
                        if (f < 1)
                            Debug.Log(f + "Attempt: " + randomPoint + "   " + NavMesh.GetAreaFromName(areaName));
                        randomcircle = (Random.insideUnitCircle * sEncounter.Area[eSet]);
                        randomPoint = sEncounter.Position[eSet]+transform.position + new Vector3(randomcircle.x, 0, randomcircle.y);

                        if (NavMesh.SamplePosition(randomPoint, out hit, 10, -1))
                        {
                            sucess = true;
                            gm.EnemyList.Add(Instantiate(sEncounter.EnemySet[eSet], hit.position, Quaternion.identity));
                        }

                    }
                }
            }

            Timer_SemiEncounter = sEncounter.Time;
            if (Current_SemiEncounter < Encounter.SemiEncounters.Count - 1)
                Current_SemiEncounter++;
            else
                Current_SemiEncounter = 0;
        }
    }


    public void ReaperSpawn()
    {
        int r = Random.Range(0, 100);

        if (r < gm.ReaperSpawn)
        {
            NavMeshHit hit;

            Vector2 randomcircle = (Random.insideUnitCircle * distance);
            Vector3 randomPoint = spawnpoint.position + new Vector3(randomcircle.x, 0, randomcircle.y);
            bool sucess = false;
            int f = 600;
            while (!sucess && f > 0)
            {
                f--;
                if (f < 1)
                    Debug.Log(f + "Attempt: " + randomPoint + "   " + NavMesh.GetAreaFromName(areaName));
                randomcircle = (Random.insideUnitCircle * distance);
                randomPoint = spawnpoint.position + new Vector3(randomcircle.x, 0, randomcircle.y);

                if (NavMesh.SamplePosition(randomPoint, out hit, 10, -1))
                {
                    sucess = true;
                    gm.EnemyList.Add(Instantiate(REAPER, hit.position, Quaternion.identity));
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            activated = true;
            if (Encounter != null)
            {
                int R = Random.Range(0, PossibleEncounters.Count - 1);
                Encounter = PossibleEncounters[R];
                Debug.Log("Spwan" + other.name + other.transform.position);
                SpawnEnemies();
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    public void Stop()
    {
        activated = false;
        GetComponent<BoxCollider>().enabled = true;
    }
}
