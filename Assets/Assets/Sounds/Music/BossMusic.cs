using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    AudioSource audio;
    [SerializeField] CheckIfPlayerPassBy check;
    bool canPlay,isPlaying;
    [SerializeField] BossScript boss;

    void Start()
    {
        audio = GetComponent<AudioSource>();

    }

    void Update()
    {
       if(check.canPlayAudio)
        {
            canPlay = true;
            isPlaying = true;
            check.canPlayAudio = false;
          
        }
        if(canPlay)
        {
            if (Time.deltaTime != 0)
            {
                if (isPlaying)
                {
                    audio.Play();
                    isPlaying = false;
                }
            }
            else if (Time.deltaTime == 0)
            {
                audio.Pause();
                isPlaying = true;
            }
        }
        if (boss.isDead) audio.Stop();
    }
}
