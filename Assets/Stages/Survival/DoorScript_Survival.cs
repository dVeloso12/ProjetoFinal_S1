using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript_Survival : MonoBehaviour
{

    [SerializeField] ChestScript chest;

    bool doorOpened;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompleteSurvival()
    {
        this.gameObject.SetActive(false);
        chest.canAppear = true;
        doorOpened = true;
    }

}
