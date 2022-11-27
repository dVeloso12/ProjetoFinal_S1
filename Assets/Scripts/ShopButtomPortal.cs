using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtomPortal : MonoBehaviour
{
    [SerializeField] GameObject Portal;
    GameplayOrganize game;
    public bool cantGenerateMore = false;
    public bool opened = false;
    [SerializeField] TMPro.TextMeshProUGUI readyToGenerate, GeneratedFinished;
    public bool inTutorial;


    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameplayOrganize>();
    }
    private void Update()
    {
        UpdateScreenUI();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!inTutorial)
        {
            if (other.transform.name == "Player" && Input.GetKey(KeyCode.H) && !cantGenerateMore)
            {
                Portal.SetActive(true);
                game.ResetGenerator();
                cantGenerateMore = true;
                opened = true;

            }
        }
        else
        {
            if (other.transform.name == "Player" && Input.GetKey(KeyCode.H) && !cantGenerateMore)
            {
                Portal.SetActive(true);
                cantGenerateMore = true;
                opened = true;

            }
        }
    }

    void UpdateScreenUI()
    {
        if (opened)
        {
            readyToGenerate.enabled = false;
            GeneratedFinished.enabled = true;
        }
        else
        {
            GeneratedFinished.enabled = false;
            readyToGenerate.enabled = true;

        }
    }
}
