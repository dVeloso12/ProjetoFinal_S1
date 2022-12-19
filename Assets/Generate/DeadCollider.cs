using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCollider : MonoBehaviour
{
    public bool isTutorial;
    GameplayOrganize gm;
    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameplayOrganize>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "Player")
        {
            if (isTutorial) gm.GoToStage();
            else
                other.GetComponent<Player>().isdead = true;
        }
    }
}
