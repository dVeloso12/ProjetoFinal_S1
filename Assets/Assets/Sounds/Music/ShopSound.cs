using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSound : MonoBehaviour
{
    AudioSource audio;
    bool isPlaying, doAudio;
    float soundTimer;
    private void Start()
    {
        audio = GetComponent<AudioSource>();

    }

    private void Update()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            isPlaying = true;
            doAudio = true;
        }
    }
}
