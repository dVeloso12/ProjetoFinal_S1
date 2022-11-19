using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ComputerLobby : MonoBehaviour
{
    public bool canDisable;
    public bool canChangeScreen;
    public bool pcMode;
    public bool runReady;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && Input.GetKey(KeyCode.H) && !pcMode && !runReady)
        { 
           
                other.GetComponent<CameraSwitch>().Register(GameObject.Find("LobbyScreenCam").GetComponent<CinemachineVirtualCamera>());
                other.GetComponent<CameraSwitch>().SwitchCamera(GameObject.Find("LobbyScreenCam").GetComponent<CinemachineVirtualCamera>());
                pcMode = true;
                canDisable = true;
            canChangeScreen = true;
        }
    }


    public void LeavePc()
    {
        GameObject.Find("Player").GetComponent<CameraSwitch>().SwitchCamera(GameObject.Find("PlayerVCam").GetComponent<CinemachineVirtualCamera>());
        GameObject.Find("Player").GetComponent<CameraSwitch>().Unregister(GameObject.Find("LobbyScreenCam").GetComponent<CinemachineVirtualCamera>());
    }


}
