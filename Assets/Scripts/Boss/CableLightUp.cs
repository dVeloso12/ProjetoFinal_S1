using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableLightUp : MonoBehaviour
{
    [SerializeField] Material cableLight;
    [SerializeField] GameObject plate;

    public bool canLightUp;
    public float speed;

    float timerLight;

    private void Start()
    {
        resetValues();
    }

    void Update()
    {
        if (canLightUp) cableLight.SetFloat("_LightUp", 1);
        else cableLight.SetFloat("_LightUp", 0);
    }

    public void IncreaseLight()
    {
        canLightUp = true;
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
