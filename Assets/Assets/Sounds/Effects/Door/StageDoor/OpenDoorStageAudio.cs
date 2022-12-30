using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorStageAudio : MonoBehaviour
{
    enum TypeStage
    {
        Deff,Survive,Payload,Boss,ShopMachine
    };
    [SerializeField] TypeStage type;
    AudioSource audio;
    [SerializeField] AudioClip clip;
    [Header("Doors")]
    [SerializeField] PlateScript plateScrpDeff;
    [SerializeField] SurvivalScript SurvScrp;
    [SerializeField] Payload PayloadScrp;
    [SerializeField] BossScript bossScrp;
    [SerializeField] ShopButtomPortal ShopScrp;


    [SerializeField] CloseDoorsBehind close;
    int canPlayAudio;
    


    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
             
            if (type == TypeStage.Deff)
            {
                if (plateScrpDeff.PlateCompleted && canPlayAudio == 0)
                {
                    audio.PlayOneShot(clip);
                    canPlayAudio++;

                }
                if (close.playSound && canPlayAudio == 1)
                {
                    audio.PlayOneShot(clip);
                    canPlayAudio++;
                }

            }else if(type == TypeStage.Survive)
            {
            if (SurvScrp.completed && canPlayAudio == 0)
            {
                audio.PlayOneShot(clip);
                canPlayAudio++;
            }
            if (close.playSound && canPlayAudio == 1)
            {
                audio.PlayOneShot(clip);
                canPlayAudio++;
            }
        }else if (type == TypeStage.Payload)
        {
            if (PayloadScrp.Ended && canPlayAudio == 0)
            {
                audio.PlayOneShot(clip);
                canPlayAudio++;
            }
            if (close.playSound && canPlayAudio == 1)
            {
                audio.PlayOneShot(clip);
                canPlayAudio++;
            }
        }else if(type == TypeStage.Boss)
        {
            if(bossScrp.isDead && canPlayAudio == 0)
            {
                audio.PlayOneShot(clip);
                canPlayAudio++;
            }
        }else if (type == TypeStage.ShopMachine)
        {
            if(ShopScrp.opened && canPlayAudio == 0)
            {
                audio.PlayOneShot(clip);
                canPlayAudio++;
            }
        }

    }
}
