using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Encounter", menuName = "ScriptableObjects/Encounter", order = 2)]
public class Encounters : ScriptableObject
{
  
    [SerializeField] public List<Enemy_AI_2> EnemySet;
    [SerializeField] public List<int> Quantity;
    [SerializeField] public List<Vector3> Position;
    [SerializeField] public List<float> Time;
    [SerializeField] public List<int> Area;


}
