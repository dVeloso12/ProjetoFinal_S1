using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviour
{
    [Header("General Stuff")]
    [SerializeField] public float PlayerHp;
    float saveMaxHP;
    public bool isdead;
    GameManager gm;

    Image hp_head,hp_bar;

    CinemachineVirtualCamera vCam;

    [SerializeField] float mouseSensivity;

    float previousMouseSensivity;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        hp_head = GameObject.Find("Bar-HpHead").GetComponent<Image>();
        hp_bar = GameObject.Find("Hp-Bar").GetComponent<Image>();
        saveMaxHP = PlayerHp;

        GameObject temp = gameObject.transform.parent.gameObject;

      
        if (temp == null) Debug.LogWarning("obj is null");

        //foreach(Transform obj in temp.GetComponentsInChildren<Transform>())
        //{

        //    if(obj.gameObject.name == "PlayerVCam")
        //    {
        //        Debug.Log("Found Camera");
        //        vCam = obj.GetComponent<CinemachineVirtualCamera>();
        //    }
        //}

        vCam = temp.GetComponentInChildren<CinemachineVirtualCamera>();

        if (vCam == null) Debug.LogWarning("virtual camera e nula");

        mouseSensivity = vCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed;
        previousMouseSensivity = mouseSensivity;
    }

    
    void Update()
    {
        if (PlayerHp > saveMaxHP)
            PlayerHp = saveMaxHP;
        UpdateUI();
        if (isdead)
        {
            gm.GameOver();
            isdead = false;
        }

        if(mouseSensivity != previousMouseSensivity)
        {
            vCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = mouseSensivity;
            vCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = mouseSensivity;

            previousMouseSensivity = mouseSensivity;

        }

    }

    void UpdateUI()
    {
        hp_bar.fillAmount = (PlayerHp+17) * 0.74f / saveMaxHP;
        hp_head.fillAmount = (PlayerHp+ 20) * 0.824f / saveMaxHP;
    }

    public void Damage(float dmg)
    {
        PlayerHp -= dmg;
        Debug.LogWarning("Player Hit. HP : " + PlayerHp);
        if(PlayerHp <= 0)
        {
            
            gm.GameOver();
        }
    }

}
