using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivalScript : MonoBehaviour
{
    [SerializeField] DoorScript_Survival survivalDoor;
    [SerializeField]public int nObjective;
    public TextMeshProUGUI SurvText;
    float CompletePerc;
    bool ReaperAttempt=true;
    public bool completed;

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
        if(gm.DifficultyMod<=3)
        nObjective += (int)((gm.DifficultyMod - 1) * 10);
        else
            nObjective += (int)((3 - 1) * 10);
    }

    void Update()
    {
        if (gm.surv)
        {
            SurvText.text = "Kill " + nObjective+ " enemies.";
            if (nObjective <= 0)
            {
                survivalDoor.CompleteSurvival();
                completed = true;
                SurvText.text = "Stage completed!";
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
