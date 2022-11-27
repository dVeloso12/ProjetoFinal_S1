using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPortal : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI readyToGenerate, GeneratedFinished;
    GameplayOrganize game;
    [SerializeField] ShopButtomPortal bt;
    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameplayOrganize>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.transform.name == "Player")
        {
            gameObject.SetActive(false);
            bt.cantGenerateMore = false;
            bt.opened = false;
            game.tpPlayerToLimbo();
        }

    }

   
}
