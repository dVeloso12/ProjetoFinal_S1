using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    enum SpawnType { AllMap, Localized }

    [SerializeField] SpawnType spawnType;

    [SerializeField] bool ToSpawnEnemies;

    [SerializeField] GameObject enemiesPrefab;
    [SerializeField] GameObject enemiesParent;

    [SerializeField] int enemiesToSpawnQuantity;
    [SerializeField] int RaycastHeight;

    [SerializeField] LayerMask groundLayerMask;

    [SerializeField] List<GameObject> spawners;

    [SerializeField] int nSpawners;

    Pathfinder pathfinderInstance;
    EnemyManager enemyManagerInstance;

    public List<GameObject> activeEnemiesList;

    // Start is called before the first frame update
    void Start()
    {
        pathfinderInstance = Pathfinder.Instance;
        enemyManagerInstance = EnemyManager.Instance;  
        MapGrid<PathNode> grid = pathfinderInstance.GetGrid();
        activeEnemiesList = new List<GameObject>();
        //List<Vector3> spawnerPos = new List<Vector3>();

        //for(int i = 0; i < nSpawners; i++)
        //{

        //    int gridLenght = grid.GetWidth() * grid.GetHeight();



        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (ToSpawnEnemies)
        {

            if (spawnType == SpawnType.AllMap)
                SpawnEnemies();
            else
                SpawnEnemiesLocalized();
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

            GameObject tempEnemy = Instantiate(enemiesPrefab, position, rotationToSpawn, enemiesParent.transform);
            enemyManagerInstance.ActivateEnemy(tempEnemy);

        }

        ToSpawnEnemies = false;
    }

    public void SpawnEnemiesLocalized()
    {
        
        foreach(GameObject spawner in spawners)
        {
            Spawner spawnerSCript = spawner.GetComponent<Spawner>();
            List<Vector3> positionsToSpawn = GetPositionsToSpawn(spawner.transform.position, spawnerSCript.radius, spawnerSCript.enemiesToSpawn_Quantity);
            System.Random rand = new System.Random();

            if (positionsToSpawn == null) Debug.Log("Positions to spawn e nulo");

            foreach(Vector3 pos in positionsToSpawn)
            {

                float rotY = rand.Next(-180, 180) + (float) rand.NextDouble();
                Quaternion rot = new Quaternion(0, rotY, 0, 0);

                GameObject tempEnemy = Instantiate(enemiesPrefab, pos, rot, enemiesParent.transform);
                enemyManagerInstance.ActivateEnemy(tempEnemy);

            }
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

    public List<Vector3> GetPositionsToSpawn(Vector3 spawnerPosition, float spawnerRadius, int enemiesToSpawn_Quantity)
    {
        List<Vector3> positionsToSpawn = new List<Vector3>();
        bool canSpawn = false;
        System.Random rand = new System.Random();  

        for(int i = 0; i < enemiesToSpawn_Quantity; i++)
        {
            Vector3 tempPosition = new Vector3();
            while (!canSpawn)
            {

                float randX = spawnerPosition.x + rand.Next(0, (int)spawnerRadius) * rand.Next(-1, 1) + (float)rand.NextDouble(), 
                    randZ = spawnerPosition.z + rand.Next(0, (int)spawnerRadius) * rand.Next(-1, 1) * (float)rand.NextDouble();

                tempPosition = new Vector3(randX, RaycastHeight, randZ);

                RaycastHit hitInfo;

                if(Physics.Raycast(tempPosition, -Vector3.up, out hitInfo, 100f, groundLayerMask))
                {

                    tempPosition.y = hitInfo.point.y;
                    canSpawn = true;

                }

            }

            if(tempPosition != null)
                positionsToSpawn.Add(tempPosition);

        }


        return positionsToSpawn;
    }

    public void AddEnemiesToList(GameObject enemyToAdd)
    {

        activeEnemiesList.Add(enemyToAdd);

    }

    public void RemoveEnemiesFromList(GameObject enemyToRemove)
    {

        activeEnemiesList.Remove(enemyToRemove);

    }


}
