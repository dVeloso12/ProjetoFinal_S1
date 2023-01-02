using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrMachineTp : MonoBehaviour
{
    public GameplayOrganize game;
    void Start()
    {
        game = GameObject.FindObjectOfType<GameplayOrganize>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            game.toGame = true;
            
        }
    }
}
