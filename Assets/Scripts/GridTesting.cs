using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class GridTesting : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] float cellSize;

    MapGrid<PathNode> grid; 

    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = new Pathfinder(width, height, cellSize);
    }

    // Update is called once per frame
    void Update()
    {

        

    }
}
