using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivalScript : MonoBehaviour
{
    [SerializeField] DoorScript_Survival survivalDoor;
    [SerializeField]public int nObjective;
    TextMeshProUGUI SurvText;

    GameManager gm;

    public bool isSurvivalCompleted;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        SurvText = GameObject.Find("SurvText").GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (gm.surv)
        {
            SurvText.text = "To Kill: " + nObjective;
            if (nObjective <= 0)
            {
                survivalDoor.CompleteSurvival();
                SurvText.text = "COMPLETED";
            }
        }
        else
            SurvText.text = "";

    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gm.surv = this;
            gm.SurvStage = true;
        }
    }


}
