using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameplayOrg : MonoBehaviour
{
    [SerializeField] GameObject game;
    void Start()
    {
        Instantiate(game);
    }
}
 
