using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    private const int MOVE_STRAIGHT_COST = 10, MOVE_DIAGONAL_COST = 14;


    MapGrid<PathNode> grid;

    List<PathNode> openList, closedList;
    
    public Pathfinder(int widtth, int height, float cellSize)
    {

        grid = new MapGrid<PathNode>(widtth, 0, height, cellSize, Vector3.zero, (MapGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));

    }

    private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {

        PathNode startNode = grid.GetGridObj(startX, startY);
        PathNode endNode = grid.GetGridObj(endX, endY);

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
                return CalculatePath(endNode);
            }


            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(PathNode neighborNode in GetNeighborNodes(currentNode))
            {
                if (closedList.Contains(neighborNode)) continue;

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

        //Out of nodes on openList. No available path
        return null;

    }

    private List<PathNode> GetNeighborNodes(PathNode currentNode)
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

    private PathNode GetNode(int x, int y)
    {
        return grid.GetGridObj(x, y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {

        return null;

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
