using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class GridTesting : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] float cellSize;

    [SerializeField] private HeatMapBoolVisual heatMapVisual;

    MapGrid<bool> testingGrid; 

    // Start is called before the first frame update
    void Start()
    {

        testingGrid = new MapGrid<bool>(width, 0, height, cellSize, Vector3.zero);

        heatMapVisual.SetGrid(testingGrid);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Vector3 position = UtilsClass.GetMouseWorldPosition();
            Debug.Log("Clicking on Position : " + position);
            testingGrid.SetValue(position, true);

        }

        if (Input.GetMouseButtonDown(1))
        {

            Vector3 position = UtilsClass.GetMouseWorldPosition();
            Debug.Log("2 : Clicking on Position : " + position);
            testingGrid.SetValue(position, false );

        }

    }
}
