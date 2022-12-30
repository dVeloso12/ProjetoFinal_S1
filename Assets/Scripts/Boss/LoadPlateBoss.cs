using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlateBoss : MonoBehaviour
{

    [SerializeField] GameObject Plate;
    [SerializeField] GameObject Crystal;

    bool onPlate,onMenu;
    AudioSource audio;
    bool startAudio, StopAudio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        StopAudio = false;
    }
    private void Update()
    {
        AudioManager();
    }

    void AudioManager()
    {
        if(Time.deltaTime == 0)
        {
            audio.Stop();
            onMenu = true;
        }
        else if (Time.deltaTime != 0 && onPlate && onMenu)
        {
            startAudio = true;
            onMenu = false;
        }

        if (startAudio)
        {
            audio.Play();
            startAudio = false;
        }
        if (StopAudio || Plate.GetComponent<PlateBossRoom>().plateCompleted)
        {
            audio.Stop();
            StopAudio = false;
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Plate.GetComponent<PlateBossRoom>().canIncreaseBeam = true;
            startAudio = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Plate.GetComponent<PlateBossRoom>().canIncreaseBeam = false;
            onPlate = false;
            if (!Plate.GetComponent<PlateBossRoom>().plateCompleted)
            {
                StopAudio = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            onPlate = true;
            Plate.GetComponent<PlateBossRoom>().IncreaseLoadPlate();
            if(Plate.GetComponent<PlateBossRoom>().plateCompleted == false) Crystal.GetComponent<SpinCrystal>().Spin();
        }
    }
}
