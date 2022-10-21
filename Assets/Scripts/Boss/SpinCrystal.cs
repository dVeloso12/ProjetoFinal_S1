using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCrystal : MonoBehaviour
{
    
    public float Speed;
    public void Spin()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * Speed, 0f));
    }
}
