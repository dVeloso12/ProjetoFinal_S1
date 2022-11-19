using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript_Survival : MonoBehaviour
{

    [SerializeField] ChestScript chest;

    bool doorOpened;

    public void CompleteSurvival()
    {
        this.gameObject.SetActive(false);
        chest.canAppear = true;
        doorOpened = true;
    }

}
