using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivalScript : MonoBehaviour
{
    [SerializeField] DoorScript_Survival survivalDoor;
    [SerializeField]public int nObjective;
    TextMeshProUGUI SurvText;
    float CompletePerc;
    bool ReaperAttempt=true;

    GameManager gm;

    public bool isSurvivalCompleted;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        SurvText = GameObject.Find("SurvText").GetComponent<TextMeshProUGUI>();
        CompletePerc = nObjective;
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

            if (ReaperAttempt && nObjective <= CompletePerc * .2f)
            {
                GetComponent<StageSpawner>().ReaperSpawn();
                ReaperAttempt = false;
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
