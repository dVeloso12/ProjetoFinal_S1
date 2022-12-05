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

    public void HSMod_50()
    {
        gm.HSMod += .5f;
    }
    public void HSMod_100()
    {
        gm.HSMod += 1f;
    }
    public void Damage_10()
    {
        gm.DamageMod +=0.1f;
    }
    public void MovSpeed_10()
    {
        gm.MoveSpeedMod += 0.1f;
    }
    public void MovSpeed_20()
    {
        gm.MoveSpeedMod += 0.2f;
    }

    public void FireRate_10()
    {
        gm.FireRateMod += 0.1f;
    }

    public void FireRate_15()
    {
        gm.FireRateMod += 0.15f;
    }

    public void FireRate_20()
    {
        gm.FireRateMod += .2f;
    }

    public void ClipSize_20()
    {
        gunc.AmmoClipSize =(int)(gunc.AmmoClipSize* 1.2f);
        gunc.Reloaded();
    }

    public void CD_10()
    {
        gm.CDMod += .1f;
    }

    public void CD_20()
    {
        gm.CDMod += .2f;
    }
}
