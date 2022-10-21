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
        cableLight.SetFloat("_Fill", 0);
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
        if(timerLight >= 2f)
        {
            timerLight = 2f;
            plate.GetComponent<PlateBossRoom>().plateReady = true;
        }
    }
    public void resetValues()
    {
        timerLight = 0f;
        cableLight.SetFloat("_Fill", 0);
        canLightUp = false;
    }
}
