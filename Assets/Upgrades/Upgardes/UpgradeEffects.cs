using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEffects : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gm;

    GunController gunc;
    Shotgun shotgun;
    Pistol pistol;
    AssaultRifle assaultRifle;
    Player player;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gunc = FindObjectOfType<GunController>();
        shotgun = FindObjectOfType<Shotgun>();
        pistol = FindObjectOfType<Pistol>();
        assaultRifle = FindObjectOfType<AssaultRifle>();
        player = FindObjectOfType<Player>();
        
    }

    #region Genreal

    #region GeneralGun
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
        gunc.AmmoClipSize = (int)(gunc.AmmoClipSize * 1.2f);
        gunc.Reloaded();
    }
    #endregion

    #region GeneralStats
    public void MovSpeed_10()
    {
        gm.MoveSpeedMod += 0.1f;
    }
    public void MovSpeed_20()
    {
        gm.MoveSpeedMod += 0.2f;
    }

    public void CD_10()
    {
        gm.CDMod += .1f;
    }

    public void CD_20()
    {
        gm.CDMod += .2f;
    }

    public void Health_10() {
        player.saveMaxHP *= Mathf.FloorToInt(1.1f);
        player.PlayerHp *= Mathf.FloorToInt(1.1f);
    }

    public void Health_30()
    {
        player.saveMaxHP *= Mathf.FloorToInt(1.3f);
        player.PlayerHp *= Mathf.FloorToInt(1.3f);
    }

    public void Money_20()
    {
        gm.MoneyMod += .2f;
    }

    #endregion
    #endregion


    #region WeaponSpecifics
    #region Pistol

    public void PistolCrit_5()
    {
        pistol.CritRate += 5;
        if(pistol.CritRate>=90)
        gm.RemoveUgrade(GetComponent<Upgrade>().indexu);
    }

    public void PistolCrit_10()
    {
        pistol.CritRate += 10;
        if (pistol.CritRate >= 90)
            gm.RemoveUgrade(GetComponent<Upgrade>().indexu);
    }

    public void PistolCritD_10()
    {
        pistol.CritDmg += .1f;
    }

    public void PistolCritD_50()
    {
        pistol.CritDmg += .5f;
    }


    #endregion


    #region Shotgun

    public void ShotgunPellets_1()
    {
        shotgun.Pellets++;
    }

    public void ShotgunDistance_5()
    {
       shotgun.Distance += 5;
        if (shotgun.Distance >= 30f)
            gm.RemoveUgrade(GetComponent<Upgrade>().indexu);
    }

    public void ShotgunSpread_10()
    {
        shotgun.Spread-=.1f;
        if (shotgun.Spread <= .1f)
            gm.RemoveUgrade(GetComponent<Upgrade>().indexu);
}

    public void PowerShotCD()
    {
        shotgun.PowerShotCD -= .5f;
        if(shotgun.PowerShotCD<=.5f)
            gm.RemoveUgrade(GetComponent<Upgrade>().indexu);
    }



    #endregion


    #region AR


    public void SniperPiercing_1()
    {
        assaultRifle.piercing++;
        if (assaultRifle.piercing >= 5)
            gm.RemoveUgrade(GetComponent<Upgrade>().indexu);
    }


    public void SniperDmgMult_20()
    {
        assaultRifle.SniperMult += .2f;
    }


    public void SniperCD_20()
    {
        assaultRifle.SniperCD -= .4f;
        if(assaultRifle.SniperCD<.4f)
            gm.RemoveUgrade(GetComponent<Upgrade>().indexu);
    }
    #endregion
    #endregion

    #region Skills

    public void IceNade()
    {
        FindObjectOfType<Granade>().Ice = true;
        gm.RemoveUgrade(GetComponent<Upgrade>().indexu);

    }

    public void NadePower_10()
    {
        FindObjectOfType<Granade>().power += 10;
    }


    #endregion
}
