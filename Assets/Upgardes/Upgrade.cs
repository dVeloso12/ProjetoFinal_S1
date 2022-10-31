using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Upgrade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private Vector3 OriginScale;

    private UpgradeEffects Effects;

    public bool General = true;

    public WeaponType WeaponAffected;

    public string EffectName;


    public string Description;

    public TextMeshProUGUI TextDescription;

    [HideInInspector]
    public int price;

    GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        OriginScale = transform.localScale;
        Effects = GetComponent<UpgradeEffects>();

        TextDescription.text = Description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale *= 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = OriginScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        

        if (gm.gameState == 0)
        {
            Effects.Invoke(EffectName, 0);
            gm.CloseAddUpgrade();

        }
        else
        {
            if (gm.Money >= price)
            {
                Effects.Invoke(EffectName, 0);
                gm.Money -= price;
                Destroy(gameObject);
            }
        }
    }

    
}
