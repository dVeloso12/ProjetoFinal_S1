using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateBossRoom : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] Material PlateLoad;
    [SerializeField] Material BeamCrystal;
    [Header("Materials")]
    [SerializeField] GameObject CablePlate;
    [Header("General Stuff")]
    public bool plateCompleted;
    [SerializeField] public bool _resetValues;
    [SerializeField] public bool plateReady;


    public float speed;
    float beamTimer,plateTimer;
    public bool canIncreaseBeam;
    bool startPlate;

    private void Start()
    {
        BeamCrystal.SetFloat("_Fill", 0f);
        PlateLoad.SetFloat("_Fill", 0f);
    }

    void Update()
    {
        if (canIncreaseBeam && !plateCompleted) IncreaseBeam();
        else if((!canIncreaseBeam && !plateCompleted)||(plateCompleted))DecreaseBeam();

        if (plateCompleted) CablePlate.GetComponent<CableLightUp>().IncreaseLight();

        if(_resetValues)
        {
            resetValues();
            _resetValues = false;
        }
        

    }
    public void resetValues()
    {
        plateCompleted = false;
        plateTimer = 0f;
        plateReady = false;
        PlateLoad.SetFloat("_Fill", 0f);
        CablePlate.GetComponent<CableLightUp>().resetValues();

    }
    #region Beam
    void IncreaseBeam()
    {
        if(beamTimer <= 2f)
        {
             beamTimer += Time.deltaTime * 10f;
             BeamCrystal.SetFloat("_Fill", beamTimer); 
            if(beamTimer >= 2f)
            {
                beamTimer = 2f;
                startPlate = true;
                
            }
        }
       
    }
    void DecreaseBeam()
    {
        if (beamTimer >= 0f)
        {
            startPlate = false;
            beamTimer -= Time.deltaTime * 10f;
            BeamCrystal.SetFloat("_Fill", beamTimer);
            if (beamTimer <= 0f)
            {
                beamTimer = 0f;
            }
        }
    }
    #endregion
    #region Plate Load
    public void IncreaseLoadPlate()
    {
        if(startPlate)
        {
            plateTimer += Time.deltaTime * speed;
            if (plateTimer >= 1.48f)
            {
                plateTimer = 1.49f;
                plateCompleted = true;
            }
            PlateLoad.SetFloat("_Fill", plateTimer);
        }
    }
    #endregion
}
