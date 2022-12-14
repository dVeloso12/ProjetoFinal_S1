using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingEffects : MonoBehaviour
{

    BulletTypes effect;
    bool isUnderEffect;

    float effectDuration, effectCD, effectDMG;

    float currentTimer, timePassed;
    float poisonRange;


    EnemyStatus statusScript;
    Enemy_AI_2 AIScript;

    public float PoisonRange
    {
        get { return poisonRange; }
    }
    public BulletTypes Effect
    {
        get { return effect; }
    }

    private void Start()
    {
        isUnderEffect = false;
        timePassed = 0f;

        statusScript = gameObject.GetComponent<EnemyStatus>();
        AIScript = gameObject.GetComponent<Enemy_AI_2>();
    }

    private void Update()
    {
        if (isUnderEffect)
        {

            if(effectDuration > 0f)
            {
                float errorMargin = 0.1f;
                
                //Debug.Log("Result : " + (timePassed - effectCD).ToString());

                if((timePassed - effectCD <= errorMargin && timePassed - effectCD >= 0f) || (timePassed - effectCD >= errorMargin && timePassed - effectCD <= 0f))
                {
                    ActivateEffect();
                    timePassed = 0f;
                }


                timePassed += Time.deltaTime;
                currentTimer -= Time.deltaTime;
            }
            else
            {
                StopEffect();
            }


           
        }
    }


    public void StartEffect(BulletTypes bulletType, float effectDuration, float effectCD, float effectDMG, float poisonRange)
    {

        effect = bulletType;
        isUnderEffect = true;

        this.effectDuration = effectDuration;
        this.effectCD = effectCD;
        this.effectDMG = effectDMG;
        this.poisonRange = poisonRange;

        currentTimer = effectDuration;
        timePassed = 0f;

        Debug.LogWarning("Effect Started in " + gameObject.name);
    }

    public void SpreadPoison(GameObject target)
    {

        target.GetComponent<DamagingEffects>().StartEffect(effect, effectDuration, effectCD, effectDMG, poisonRange);

    }

    public void StopEffect()
    {
        isUnderEffect = false;
    }

    public void ActivateEffect()
    {
        switch (effect)
        {
            case BulletTypes.IceBullet:

                AIScript.Speed /= effectDMG;
                IceVFX();

                StartCoroutine(StopIceEffect());

                break;
            case BulletTypes.FireBullet:

                Debug.LogWarning("Fire EFfect DMG");

                statusScript.Damage(effectDMG);
                FireVFX();

                break;
            case BulletTypes.PoisonBullet:

                statusScript.Damage(effectDMG);
                PoisonVFX();

                break;
        }
    }

    IEnumerator StopIceEffect()
    {
        yield return new WaitForSeconds(effectDuration);

        AIScript.Speed *= effectDMG;
    }


    public void FireVFX()
    {

    }
    public void PoisonVFX()
    {

    }
    public void IceVFX()
    {

    }
}


