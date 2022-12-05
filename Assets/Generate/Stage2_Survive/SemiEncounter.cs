using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SemiEncounter", menuName = "ScriptableObjects/SemiEncounter", order = 3)]
public class SemiEncounter : ScriptableObject
{
  
    [SerializeField] public List<GameObject> EnemySet;
    [SerializeField] public List<int> Quantity;
    [SerializeField] public List<Vector3> Position;
    
    [SerializeField] public List<int> Area;
    [SerializeField] public float Time;


}
