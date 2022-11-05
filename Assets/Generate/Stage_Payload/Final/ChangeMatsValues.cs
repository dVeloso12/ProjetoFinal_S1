using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMatsValues : MonoBehaviour
{

    [SerializeField] Material Energy;
    public float Intensity;
    public float Speed = 1f;
    bool increase = true;

    // Update is called once per frame
    void Update()
    {
        setValues();
        Color emissiveColor = Color.green;
        Energy.SetVector("_EmissionColor", Color.red * Intensity);
    }

    void setValues()
    {
       if (Intensity >= 1f)
        {
            increase = false;
        }
       else if ( Intensity < -0.1f)
        {
            increase = true;

        }
       if(increase)
        {
            Intensity += Time.deltaTime * Speed;
        }
       else
        {
            Intensity -= Time.deltaTime * Speed;
        }
    }
}
