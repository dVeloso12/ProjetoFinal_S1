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

    private void Start()
    {
        CrystalMat.SetFloat("_Fill",0f);
    }
    // Update is called once per frame
    void Update()
    { 
        CrystalMat.SetFloat("_Fill", FillPorc);
        CrystalAnims();

    }
    void CrystalAnims()
    {
        if(StartAnim)
        {
            timer += Time.deltaTime;
            if(timer < 1f)transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f * timer, transform.position.z);
            transform.Rotate(new Vector3(0f, 0.2f, 0f));
        }
    }
}
