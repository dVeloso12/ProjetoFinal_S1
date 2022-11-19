using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    public  CinemachineVirtualCamera activeCam = null;

    private void Start()
    {
        Register(GameObject.Find("PlayerVCam").GetComponent<CinemachineVirtualCamera>());
    }

    public  bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == activeCam;
    }
    public  void SwitchCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = 10;
        activeCam = camera;
        foreach (CinemachineVirtualCamera c in cameras)
        {
            if (c != camera)
            {
                c.Priority = 0;
            }
        }
    }
    public  void Register(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
        Debug.Log("cam add : " + camera);
    }

    public void Unregister(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);
        Debug.Log("cam removed : " + camera);
    }

}
