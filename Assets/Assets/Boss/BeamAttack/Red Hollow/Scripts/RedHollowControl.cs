﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateBeam
{
    ChargeStart, ChargeBeam, BeamAttack, End
}
public class RedHollowControl : MonoBehaviour{
   

    [Range(0.0f, 1.0f)]
    public float hue = 0;

   public Animator animator;

    public StateBeam stateBeam;
    public bool canAttack;
    public bool isAttacking;

    [Header("Timers")]
    [SerializeField] float maxChargeStart;
    [SerializeField] float maxBeamAttack;

    [SerializeField] BeamDmg beam;
    [SerializeField] float beamDmg;



    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        beam.dmg = beamDmg;
        beam.gameObject.GetComponent<BoxCollider>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).GetComponent<HueControl>().hue = hue;

        if(canAttack)
        {
            timer += Time.deltaTime;
            if(stateBeam == StateBeam.ChargeStart)
            {
                Play_Charging();
                if(timer >= maxChargeStart)
                {
                    stateBeam = StateBeam.ChargeBeam;
                    timer = 0;
                }
            }
            if (stateBeam == StateBeam.ChargeBeam)
            {
                Finish_Charging();
                if(timer >= 0.4f)
                {
                    stateBeam = StateBeam.BeamAttack;
                    timer = 0;
                }
            }
            if(stateBeam == StateBeam.BeamAttack)
            {

                Burst_Beam();
                beam.gameObject.GetComponent<BoxCollider>().enabled = true;
                isAttacking = true;
                if (timer >= maxBeamAttack)
                {
                    stateBeam = StateBeam.End;
                    beam.gameObject.GetComponent<BoxCollider>().enabled = false;
                    timer = 0;
                }
            }
            if(stateBeam == StateBeam.End)
            {
                Dead();
                if(timer >= 1f)
                {
                    timer = 0;
                    canAttack = false;
                    isAttacking = false;
                    stateBeam = StateBeam.ChargeStart;
                    
                }
            }
        }

    }

    public void Play_Charging() {
        animator.Play("Red Hollow - Charging");
    }

    public void Finish_Charging() {
        animator.Play("Red Hollow - Charged");
    }

    public void Burst_Beam() {
        animator.Play("Red Hollow - Burst");

    }

    public void Dead()
    {
        animator.Play("Red Hollow - Dead");
    }
}
