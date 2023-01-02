using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableLightUp : MonoBehaviour
{
    [SerializeField] Material cableLight;
    [SerializeField] GameObject plate;

    AudioSource audio;

    public bool canLightUp;
    public float speed;

    float timerLight;
    bool soundOn;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        resetValues();
    }

    void Update()
    {
        if (canLightUp) cableLight.SetFloat("_LightUp", 1);
        else cableLight.SetFloat("_LightUp", 0);

        if(soundOn&&!FindObjectOfType<GameManager>().isTurorial)
        {
            audio.Play();
        }
    }

    public void IncreaseLight()
    {
        canLightUp = true;
        soundOn = true;
        timerLight += Time.deltaTime * speed;
        cableLight.SetFloat("_Fill", timerLight);
      
        if(timerLight >= 17f)
        {
            timerLight = 17f;  
            plate.GetComponent<PlateBossRoom>().plateReady = true;
        }
    }
    public void resetValues()
    {
        
        if(gameObject.name == "CableRight")
        {
            timerLight = -2f;
            cableLight.SetFloat("_Fill", -2f);
        }
        else
        {
            timerLight = 0f;
            cableLight.SetFloat("_Fill", 0);
            canLightUp = false;
        }
   
    }
}
