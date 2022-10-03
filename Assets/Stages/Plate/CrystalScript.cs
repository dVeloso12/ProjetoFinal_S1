using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{

    [SerializeField] Material CrystalMat;
    [SerializeField] public float FillPorc;
    [SerializeField] float Speed;
    public bool StartAnim;
    bool goingUp;
    float timer;

    // Update is called once per frame
    void Update()
    {
        if(FillPorc < 0) FillPorc = 0.01f;
        else if(FillPorc > 1) FillPorc = 1.01f;

        CrystalMat.SetFloat("_ProgressBorder", FillPorc*Speed);
        CrystalAnims();

    }
    void CrystalAnims()
    {
        if(StartAnim)
        {
            timer += Time.deltaTime;
            if(timer < 1f)transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            transform.Rotate(new Vector3(0f, 0.2f, 0f));
        }
    }
}
