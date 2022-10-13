using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Upgrade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private Vector3 OriginScale;

    private UpgradeEffects Effects;

    public string EffectName;
    void Start()
    {
        OriginScale = transform.localScale;
        Effects = GetComponent<UpgradeEffects>();
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
        
        Effects.Invoke(EffectName,0);
    }
    
}
