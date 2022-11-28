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

    public int Rarity = 1;

    public WeaponType WeaponAffected;

    public string EffectName;


    public string Description;

    TextMeshProUGUI TextDescription;

    [HideInInspector]
    public int price;

    GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        
        Effects = GetComponent<UpgradeEffects>();
        TextDescription = GetComponentInChildren<TextMeshProUGUI>();

        TextDescription.text = Description;


        int r2 = UnityEngine.Random.Range(-5, 5);
        price = 50 * Rarity + r2;
    }
    void Start()
    {
        OriginScale = transform.localScale;
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
