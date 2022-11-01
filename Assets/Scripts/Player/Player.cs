using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [Header("General Stuff")]
    [SerializeField] public float PlayerHp;
    float saveMaxHP;
    public bool isdead;
    GameManager gm;

    Image hp_head,hp_bar;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        hp_head = GameObject.Find("Bar-HpHead").GetComponent<Image>();
        hp_bar = GameObject.Find("Hp-Bar").GetComponent<Image>();
        saveMaxHP = PlayerHp;
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
