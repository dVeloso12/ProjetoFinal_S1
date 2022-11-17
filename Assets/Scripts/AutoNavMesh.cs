using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AutoNavMesh : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshSurface navsurf;
    private void Awake()
    {
        navsurf = GetComponent<NavMeshSurface>();

        navsurf.BuildNavMesh();


    }
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
