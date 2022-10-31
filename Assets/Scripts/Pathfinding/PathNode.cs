using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private MapGrid<PathNode> grid;

    public int x, y;

    public int gCost, hCost, fCost;

    public PathNode previousNode;

    public bool isWalkable;
    float cellSize;
    LayerMask groundLayer;

    GameObject groundCheck;
    Collider groundCheckCollider;

    public PathNode(MapGrid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;

        cellSize = grid.GetCellSize();

        isWalkable = false;
        this.groundCheck = grid.GetGroundCheck();
        groundCheckCollider= groundCheck.GetComponent<Collider>();
        groundLayer = grid.GetGroundLayer();

        CheckIfIsWalkable();
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return x + " , " + y + " : " + isWalkable.ToString();
    }

    public void SetIsWalkable(bool _isWalkable)
    {
        isWalkable = _isWalkable;
        grid.TriggerGridObjectChanged(x, y);
    }


    public void CheckIfIsWalkable()
    {
        Debug.Log("Checking if is walkable");
        Vector3 newPosition = grid.GetWorldPosition(x, y) /*+ new Vector3(1, 0, 1) * grid.GetCellSize() * 0.5f*/;

        groundCheck.transform.position = new Vector3(newPosition.x, groundCheck.transform.position.y, newPosition.y);
        isWalkable = groundCheck.GetComponent<NodeGroundCheck>().CheckIfIsWalkable(groundLayer);

        //if (isWalkable)
        //    Debug.Log(newPosition);

        //Debug.Log("Position : " + x + " , " + y);
        //Debug.Log("Is Walkable : " + isWalkable);
                

    }

}
