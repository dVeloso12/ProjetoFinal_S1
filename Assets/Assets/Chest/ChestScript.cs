using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    GameManager gm;
    public bool canAppear;
   bool canUpgrade,setcollider;


    Spawn spawnScript;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        gm = FindObjectOfType<GameManager>();
        canAppear = false;
        //spawnScript = GenerateRun.instance.EnemiesManagerInstantiated.GetComponent<Spawn>();
    }
    void Update()
    {
       if(canAppear)
        {
            GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;

        }

       if(canUpgrade)
        {
            gm.AddUpgrade();
            canUpgrade = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {

            canUpgrade = true;
            canAppear = false;
            gameObject.GetComponent<Collider>().enabled = false;

            //spawnScript.StopOverTimeSpawning();

            //for(int i = 0; i < spawnScript.activeEnemiesList.Count; i++)
            //{
            //    spawnScript.RemoveEnemiesFromList(spawnScript.activeEnemiesList[i]);
            //}

            gm.ResetStage();
        }
    }
}
