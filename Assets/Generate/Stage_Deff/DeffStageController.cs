using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeffStageController : MonoBehaviour
{
    [SerializeField] PlateScript plate;
    TextMeshProUGUI txtPlate;

    [SerializeField] bool isTutorial;
    AudioSource audio;
    bool isPlaying, doAudio;
    float soundTimer;



    void Start()
    {
        if(!isTutorial)
        {
            audio = GetComponent<AudioSource>();
            audio.volume = 0.5f;
        }

        txtPlate = GameObject.Find("DeffText").GetComponent<TextMeshProUGUI>();
        txtPlate.enabled = false;
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
                if (soundTimer > audio.clip.length)
                {
                    doAudio = true;
                    soundTimer = 0f;
                }
            }

        }
        else
        {
            audio.Pause();
            doAudio = true;
        }
    }

    void Update()
    {
        UpdateUI();
       if(!isTutorial) SoundManager();
    }
    void UpdateUI()
    {
        float value = Mathf.Round(plate.PlatePorc*100f);
        txtPlate.text = "Capture the Plate\n Plate: "+value.ToString()+"%";
    }

    public void disableObjTxt()
    {
        txtPlate.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            txtPlate.enabled = true;
            if(!isTutorial)
            {
                isPlaying = true;
                doAudio = true;
            }
        }
    }
}
