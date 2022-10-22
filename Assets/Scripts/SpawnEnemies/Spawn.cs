using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    [SerializeField] bool ToSpawnEnemies;

    [SerializeField] GameObject enemiesPrefab;
    [SerializeField] GameObject enemiesParent;

    [SerializeField] int enemiesToSpawnQuantity;
    [SerializeField] int RaycastHeight;

    [SerializeField] LayerMask groundLayerMask;

    Pathfinder pathfinderInstance;

    // Start is called before the first frame update
    void Start()
    {
        pathfinderInstance = Pathfinder.Instance;


    }

    // Update is called once per frame
    void Update()
    {
        if (ToSpawnEnemies)
        {
            SpawnEnemies();
        }   
    }


    public void SpawnEnemies()
    {
        List<Vector3> positionsToSpawn = GetPositionsToSpawn();


        foreach(Vector3 position in positionsToSpawn)
        {

            System.Random rand = new System.Random();

            float randY = rand.Next(-180, 180);

            Quaternion rotationToSpawn = new Quaternion(0, randY, 0, 0);

            Instantiate(enemiesPrefab, position, rotationToSpawn, enemiesParent.transform);

        }

        ToSpawnEnemies = false;
    }


    public List<Vector3> GetPositionsToSpawn()
    {
        List<Vector3> positionsToSpawn = new List<Vector3>();
        List<PathNode> walkableNodes = new List<PathNode>();

        MapGrid<PathNode> grid = pathfinderInstance.GetGrid();

        Debug.Log("Width : " + grid.GetWidth());
        Debug.Log("Height : " + grid.GetHeight());

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {

                PathNode node = grid.GetGridObj(x, y);

                if (node.isWalkable)
                {  
                    walkableNodes.Add(node);
                }

            }
        }

        //foreach(PathNode node in walkableNodes)
        //{

        //    Vector3 nodeWorldPosition = grid.GetWorldPosition(node.x, node.y);

        //    Debug.DrawLine(nodeWorldPosition, nodeWorldPosition + Vector3.up * 50f, Color.red, 100f);
        //}

        int gridLenght = walkableNodes.Count - 1;

        for(int i = 0; i < enemiesToSpawnQuantity; i++)
        {
            bool canSpawn = false;
            float cellSize = grid.GetCellSize();
            System.Random rand = new System.Random();
            Vector3 worldPosition = Vector3.zero;

            int counter = 0;

            while (!canSpawn/* || counter < 1000*/)
            {

                int nodeNum = rand.Next(0, gridLenght);
                //Debug.Log("Node Num : " + nodeNum);

                //Debug.Log("Node Position On Grid : " + walkableNodes[nodeNum].x + " , " + walkableNodes[nodeNum].y);

                float randX = (float)rand.NextDouble() * (float)rand.Next(-1, 1) * cellSize / 2f, randY = (float)rand.NextDouble() * (float) rand.Next(-1, 1) * cellSize / 2f;

                PathNode tempNode = walkableNodes[nodeNum];

                worldPosition = grid.GetWorldPosition(tempNode.x, tempNode.y);
                worldPosition.z = worldPosition.y;
                worldPosition.x += randX;
                worldPosition.z += randY;
                worldPosition.y = RaycastHeight;

                RaycastHit hitInfo;

                if(Physics.Raycast(worldPosition, Vector3.down,out hitInfo, 100f, groundLayerMask))
                {

                    if (hitInfo.collider.gameObject.tag == "Ground")
                    {
                        //Debug.Log("World position : " + worldPosition);

                        Vector3 direction = hitInfo.point - worldPosition;

                        //Debug.DrawLine(worldPosition, worldPosition + Vector3.down * 100f, Color.blue, 100f);
                        //Debug.DrawLine(worldPosition, worldPosition + direction, Color.red, 100f);

                        worldPosition.y = hitInfo.point.y + enemiesPrefab.transform.localScale.y / 2;
                        canSpawn = true;
                    }

                }

                counter++;
                

            }

            if (!canSpawn) Debug.Log("Can't find position");

            if(worldPosition != Vector3.zero) positionsToSpawn.Add(worldPosition);





        }



        return positionsToSpawn;
    }
}
