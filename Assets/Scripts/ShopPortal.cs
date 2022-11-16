using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPortal : MonoBehaviour
{
    public bool playerColide;
    [SerializeField] TMPro.TextMeshProUGUI readyToGenerate, GeneratedFinished;
    public bool opened;

    private void Update()
    {
        if(opened)
        {
            readyToGenerate.gameObject.SetActive(false);
            GeneratedFinished.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.transform.name == "Player")
        {
            Debug.Log(other.transform.name);
            playerColide = true;
            gameObject.SetActive(false);
            readyToGenerate.gameObject.SetActive(true);
            GeneratedFinished.gameObject.SetActive(false);
            opened = false;
            //gameObject.SetActive(false);
        }

    }
}
