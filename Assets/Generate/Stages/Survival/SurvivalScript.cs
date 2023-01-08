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

    AudioSource audio;
    bool isPlaying,doAudio;
    float soundTimer;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = 0.5f;

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
                GetComponent<StageSpawner>().activated = false;
                completed = true;
                SurvText.text = "";
                audio.Stop();
            }

            if (ReaperAttempt && nObjective <= CompletePerc * .2f)
            {
                GetComponent<StageSpawner>().ReaperSpawn();
                ReaperAttempt = false;
            }
        }
        else
            SurvText.text = "";

        SoundManager();

    }

    void SoundManager()
    {
        if (Time.deltaTime != 0)
        {
            if (isPlaying)
            {
                soundTimer += Time.deltaTime;
                if (doAudio)
                {
                    audio.Play();
                    doAudio = false;
                }
                if(soundTimer > audio.clip.length)
                {
                    doAudio = true;
                    soundTimer = 0f;
                }
            }

        }else
        {
            audio.Pause();
            doAudio = true;
        }
    }
    
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gm.surv = this;
            isPlaying = true;
            doAudio = true;
            gm.SurvStage = true;
        }
    }


}
