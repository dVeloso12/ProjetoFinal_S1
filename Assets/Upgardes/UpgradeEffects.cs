using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEffects : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gm;

    GunController gunc;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gunc = FindObjectOfType<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireRate_10()
    {
        gm.FireRateMod *= .9f;
    }

    public void Damage_10()
    {
        gm.DamageMod*=1.1f;
    }

    public void MovSpeed_10()
    {
        gm.MoveSpeedMod *= 1.1f;
    }

    public void FireRate_15()
    {
        gm.FireRateMod *= .85f;
    }

    public void FireRate_20()
    {
        gm.FireRateMod *= .8f;
    }

    public void ClipSize_20()
    {
        gunc.AmmoClipSize =(int)(gunc.AmmoClipSize* 1.2f);
        StartCoroutine(gunc.Reload(0));
    }
}
