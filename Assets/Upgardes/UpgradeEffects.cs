using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEffects : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gm;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireRate_10()
    {
        gm.FireRateMod += .1f;
    }

    public void Damage_10()
    {
        gm.DamageMod+=.1f;
    }

    public void MovSpeed_10()
    {
        gm.MoveSpeedMod += .1f;
    }

    public void Cooldown_10()
    {
        gm.CooldownReduction += .1f;
    }

   
    public void Money_10()
    {
        gm.MoneyMult += .1f;
    }

    public  void ClipSize_20()
    {
        gm.ClipModifier +=.2f;
    }
}
