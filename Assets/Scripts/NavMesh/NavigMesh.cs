using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigMesh : MonoBehaviour
{

    Pathfinder pathfinder;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = Pathfinder.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
