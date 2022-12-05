using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPortal : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI readyToGenerate, GeneratedFinished;
    GameplayOrganize game;
    [SerializeField] ShopButtomPortal bt;
    public bool inTutorial;
              
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
            if (inTutorial)
                game.tpPlayerToLimbo();
            else
                game.GoToStage();
        }

    }

   
}
