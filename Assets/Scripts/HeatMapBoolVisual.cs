using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class HeatMapBoolVisual : MonoBehaviour
{

    MapGrid<bool> heatMapGrid;
    Mesh mesh;
    bool updateMesh;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(MapGrid<bool> grid)
    {
        heatMapGrid = grid;
        UpdateHeatMapVisual();

        heatMapGrid.OnGridValueChanged += HeatMapGrid_OnGridValueChanged;
    }

    private void HeatMapGrid_OnGridValueChanged(object sender, MapGrid<bool>.OnGridValueChangedEventArgs e)
    {
        updateMesh = true;
    }

   
    // Update is called once per frame
    void LateUpdate()
    {
        if (updateMesh)
        {
            updateMesh = false;
            UpdateHeatMapVisual();
        }
    }

    private void UpdateHeatMapVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(heatMapGrid.GetWidth() * heatMapGrid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for(int x = 0; x < heatMapGrid.GetWidth(); x++)
        {
            for(int y = 0; y < heatMapGrid.GetHeight(); y++)
            {
                int index = x * heatMapGrid.GetHeight() + y;

                Vector3 quadSize = new Vector3(1, 1) * heatMapGrid.GetCellSize();

                bool gridValue = heatMapGrid.GetGridObj(x, y);
                float gridValueNormalized = gridValue ? 1f : 0f;

                Vector2 gridValueUV = new Vector2(gridValueNormalized, 0f);
                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, heatMapGrid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, gridValueUV, gridValueUV);

            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }

}
