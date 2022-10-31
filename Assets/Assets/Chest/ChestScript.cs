using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    GameManager gm;
    public bool canAppear;
   bool canUpgrade,setcollider;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        gm = FindObjectOfType<GameManager>();
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            canUpgrade = true;
            canAppear = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}