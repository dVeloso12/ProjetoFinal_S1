using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Encounter", menuName = "ScriptableObjects/Encounter", order = 2)]
public class Encounters : ScriptableObject
{
  
    [SerializeField] public List<SemiEncounter> SemiEncounters;
    [SerializeField] public float Time;



}
