using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingVisual : MonoBehaviour
{

    private MapGrid<PathNode> grid;
    private Mesh mesh;

    private bool updateMesh;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh; 
    }

    public void SetGrid(MapGrid<PathNode> grid)
    {
        this.grid = grid;
        UpdateVisual();


        grid.OnGridValueChanged += Grid_OnGridValueChanged;
    }

    private void Grid_OnGridValueChanged(object sender, MapGrid<PathNode>.OnGridValueChangedEventArgs e)
    {
        updateMesh = true;
    }

    public void UpdateVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for(int x = 0; x < grid.GetWidth(); x++)
        {
            for(int y = 0; y < grid.GetHeight(); y++)
            {
                int index = x * grid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

                PathNode node = grid.GetGridObj(x, y);

                if (node.isWalkable)
                {
                    quadSize = Vector3.zero;
                }

                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * 0.5f, 0f, quadSize, Vector3.zero, Vector3.zero);
            }
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
        

    // Update is called once per frame
    void LateUpdate()
    {
        if (updateMesh)
        {
            updateMesh = false;
            UpdateVisual();
        }
    }
}
