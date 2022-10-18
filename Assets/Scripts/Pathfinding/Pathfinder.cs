using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    private const int MOVE_STRAIGHT_COST = 10, MOVE_DIAGONAL_COST = 14;

    float raycastHeight;

    LayerMask groundLayerMask;

    MapGrid<PathNode> grid;

    List<PathNode> openList, closedList;

    public static Pathfinder Instance;
    GameObject groundCheck;

    public Pathfinder(int width, int height, float cellSize, GameObject groundCheck, float raycastHeight, LayerMask groundLayerMask)
    {

        grid = new MapGrid<PathNode>(width, 0, height, cellSize, Vector3.zero, groundCheck, groundLayerMask, (MapGrid<PathNode> g, int x, int y) => new PathNode(g, x, y)); ;
        Instance = this;
        this.groundCheck = groundCheck;
        this.raycastHeight = raycastHeight;
        this.groundLayerMask = groundLayerMask;
    }

    public LayerMask GetGroundLayer()
    {
        return groundLayerMask;
    }
    public MapGrid<PathNode> GetGrid()
    {
        return grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {

        PathNode startNode = grid.GetGridObj(startX, startY);
        PathNode endNode = grid.GetGridObj(endX, endY);

        if(startNode == null)
        {
            Debug.Log("Start Node e nula");
        }
        if(endNode == null)
        {
            Debug.Log("End node e nula");
        }

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for(int x = 0; x < grid.GetWidth(); x++)
        {
            for(int y = 0; y < grid.GetHeight(); y++)
            {

                PathNode pathNode = grid.GetGridObj(x, y);

                pathNode.gCost = int.MaxValue;

                pathNode.CalculateFCost();
                pathNode.previousNode = null;

            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();

        while(openList.Count > 0)
        {

            PathNode currentNode = GetLowestFCostNode(openList);
            if(currentNode == endNode)
            {
                //Final Node
                Debug.Log("Final Node");
                return CalculatePath(endNode);
            }


            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(PathNode neighborNode in GetNeighborNodes(currentNode))
            {
                if (closedList.Contains(neighborNode)) continue;

                if (!neighborNode.isWalkable)
                {
                    closedList.Add(neighborNode);
                    continue;
                }

                if (currentNode == null)
                {
                    Debug.Log("current Node e nula");
                }
                if (neighborNode == null)
                {
                    Debug.Log("neighbor  Node e nula");
                }

                int tentativeCost = currentNode.gCost + CalculateDistance(currentNode, neighborNode);
                if(tentativeCost < neighborNode.gCost)
                {
                    neighborNode.previousNode = currentNode;
                    neighborNode.gCost = tentativeCost;
                    neighborNode.hCost = CalculateDistance(neighborNode, endNode);
                    neighborNode.CalculateFCost();


                    if(!openList.Contains(neighborNode)) openList.Add(neighborNode);
                }
            }

        }

        Debug.Log("out of Nodes");
        //Out of nodes on openList. No available path
        return null;

    }

    public List<Vector3> FindPathPositionsOnMap(List<PathNode> pathList)
    {
        List<Vector3> PathPositionsOnMap = new List<Vector3>();

        if (pathList == null) return null;

        foreach(PathNode node in pathList)
        {

            float height = -1f;
            Debug.Log("Height : " + height);

            Vector3 worldPosition = grid.GetWorldPosition(node.x, node.y);

            RaycastHit hit;
            if(Physics.Raycast(new Vector3(worldPosition.x, raycastHeight, worldPosition.y), -Vector3.up, out hit, 100f, groundLayerMask))
            {

                height = hit.point.y + 0.5f;
                Debug.Log("tutsjsHeight : " + height);
            }

            Vector3 tempPos = grid.GetWorldPosition((int)hit.point.x, (int) hit.point.y);

            Vector3 position = new Vector3(worldPosition.x, height, worldPosition.y);

            PathPositionsOnMap.Add(position);

        }

        return PathPositionsOnMap;
    }

    public List<PathNode> GetNeighborNodes(PathNode currentNode)
    {
        List<PathNode> neighborNodes = new List<PathNode>();

        if(currentNode.x - 1 >= 0)
        {
            //Left
            neighborNodes.Add(GetNode(currentNode.x - 1, currentNode.y));

            //Left Down
            if(currentNode.y - 1 >= 0) neighborNodes.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            //Left Up
            if (currentNode.y + 1 < grid.GetHeight()) neighborNodes.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if(currentNode.x + 1 < grid.GetWidth())
        {
            //Right
            neighborNodes.Add(GetNode(currentNode.x + 1, currentNode.y));

            //Right Down
            if (currentNode.y - 1 >= 0) neighborNodes.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            //Right Up
            if (currentNode.y + 1 < grid.GetHeight()) neighborNodes.Add(GetNode(currentNode.x + 1, currentNode.y + 1));

        }

        //Down
        if (currentNode.y - 1 >= 0) neighborNodes.Add(GetNode(currentNode.x, currentNode.y - 1));
        //Up
        if (currentNode.y + 1 < grid.GetHeight()) neighborNodes.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighborNodes;
    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObj(x, y);
    }

    public List<PathNode> CalculatePath(PathNode endNode)
    {

        List<PathNode> path = new List<PathNode>();

        path.Add(endNode);
        PathNode currentNode = endNode;

        while(currentNode.previousNode != null)
        {
            path.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }

        path.Reverse();

        return path;
    }

    private int CalculateDistance(PathNode a, PathNode b)
    {

        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;

    }

    public PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];

        for(int i = 1; i < pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }


        return lowestFCostNode;
    }

}
