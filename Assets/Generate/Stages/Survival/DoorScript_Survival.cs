using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript_Survival : MonoBehaviour
{

    [SerializeField] ChestScript chest;
     BoxCollider colision;
     MeshRenderer mesh;
    bool doorOpened;
    bool playerpass;

    private void Start()
    {
        colision = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();
    }
    public void CompleteSurvival()
    {
        if(!playerpass) Hide_ShowDoor(false);
        chest.canAppear = true;
        doorOpened = true;
    }
    public void Hide_ShowDoor(bool state)
    {
        if (!state)
        {
            colision.enabled = false;
            mesh.enabled = false;
        }
        else
        {
            colision.enabled = true;
            mesh.enabled = true;
        }
    }
    public void PlayerPass(bool state)
    {
        playerpass = state;
    }

}
