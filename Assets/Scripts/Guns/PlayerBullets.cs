using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    public BulletTypes bulletType;

    [Header("Ice Effect")]
    [SerializeField] float iceEffectTimer, iceEffectCD;
    [SerializeField] float iceEffectModifier;

    [Header("Fire Effect")]
    [SerializeField] float fireEffectTimer, fireEffectCD;
    [SerializeField] float fireEffectDMG;

    [Header("Poison Effect")]
    [SerializeField] float poisonEffectTimer, poisonEffectCD;
    [SerializeField] float poisonEffectDMG;



    private void Start()
    {
        bulletType = BulletTypes.NormalBullets;

        
    }

    public void ChangeBulletType(BulletTypes changeTo)
    {
        bulletType = changeTo;
    }

    public virtual void StartBulletEffect(GameObject target)
    {

        float effectTimer = 0f;
        float effectCD = 0f;
        float damage = 0f;

        switch (bulletType)
        {
            case BulletTypes.IceBullet:
                effectTimer = iceEffectTimer;
                effectCD = iceEffectCD;
                damage = iceEffectModifier;

                break;
            case BulletTypes.FireBullet:
                effectTimer = fireEffectTimer;
                effectCD = fireEffectCD;
                damage = fireEffectDMG;

                break;
            case BulletTypes.PoisonBullet:
                effectTimer = poisonEffectTimer;
                effectCD = poisonEffectCD;
                damage = poisonEffectDMG;

                break;
        }


        DamagingEffects tempScript = target.GetComponent<DamagingEffects>();

        if(tempScript != null)
        {
            tempScript.StartEffect(bulletType, effectTimer, effectCD, damage);
        }
        

    }

}

public enum BulletTypes { NormalBullets, FireBullet, PoisonBullet, IceBullet }
