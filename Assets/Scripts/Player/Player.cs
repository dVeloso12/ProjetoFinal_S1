using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [Header("General Stuff")]
    [SerializeField] public float PlayerHp;
    float saveMaxHP;

    Image hp_head,hp_bar;
    void Start()
    {
        hp_head = GameObject.Find("Bar-HpHead").GetComponent<Image>();
        hp_bar = GameObject.Find("Hp-Bar").GetComponent<Image>();
        saveMaxHP = PlayerHp;
    }

    
    void Update()
    {
        UpdateUI();   
    }

    void UpdateUI()
    {
        hp_bar.fillAmount = (PlayerHp+17) * 0.74f / saveMaxHP;
        hp_head.fillAmount = (PlayerHp+ 20) * 0.824f / saveMaxHP;
    }

}
