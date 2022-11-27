using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcMsgs", menuName = "ScriptableObjects/NpcMsgs", order = 1)]
public class NpcMsgs : ScriptableObject
{
    [TextArea(4, 5)]
    [SerializeField] public List<string> Position_1;
    [TextArea(4, 5)]
    [SerializeField] public List<string> Position_2;
    [TextArea(4, 5)]
    [SerializeField] public List<string> Position_3;
    [TextArea(4, 5)]
    [SerializeField] public List<string> Position_4;
    [TextArea(4, 5)]
    [SerializeField] public List<string> Position_5;
    [TextArea(4, 5)]
    [SerializeField] public List<string> Position_6;
    [TextArea(4, 5)]
    [SerializeField] public List<string> Position_7;
    [TextArea(4, 5)]
    [SerializeField] public List<string> Position_8;
    [TextArea(4, 5)]
    [SerializeField] public List<string> Position_9;
    [TextArea(4, 5)]
    [SerializeField] public List<string> Position_10;
    [TextArea(4, 5)]
    [SerializeField] public List<string> Position_11;
    [TextArea(4, 5)]
    [SerializeField] public List<string> Position_12;

    [SerializeField] public List<Vector3> NpcSpawnPosition;

    
}
