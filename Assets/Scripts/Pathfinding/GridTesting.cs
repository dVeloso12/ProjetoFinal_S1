using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class GridTesting : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] float cellSize;
    [SerializeField] Vector3 originPosition;
    [SerializeField] float raycastHeight;
    [SerializeField] float speed;

    [SerializeField] LayerMask groundLayerMask;

    [SerializeField] PathfindingVisual visual;
    [SerializeField] GameObject groundCheck;
    [SerializeField] GameObject player;
    [SerializeField] Vector3 initialPosition;

    MapGrid<PathNode> grid;

    private Pathfinder pathfinder;
    private EnemyManager enemyManager;

    GameObject instantiatedPlayer;

    List<Vector3> pathPositions;

    int x, y;
    bool isWalkingTowardsPlayer;
    int enemyPathIndex;

    // Start is called before the first frame update
    void Start()
    {
        width = groundCheck.GetComponent<NodeGroundCheck>().mapWidth / 15;
        height = groundCheck.GetComponent<NodeGroundCheck>().mapHeight / 15;

        instantiatedPlayer = Instantiate(player, initialPosition, Quaternion.identity);

        //Debug.Log("Width : " + width);
        //Debug.Log("Height : " + height);

        //Debug.Log("Helo");

        pathfinder = new Pathfinder(width, height, cellSize, groundCheck, raycastHeight, originPosition, groundLayerMask);
        enemyManager = new EnemyManager(instantiatedPlayer);

        visual.SetGrid(pathfinder.GetGrid());
        //isWalkingTowardsPlayer = false;

        //List<Vector3> pathPositions = new List<Vector3>();
        //enemyPathIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetMouseButtonDown(0))
        //{

        //    Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        //    int tempX = (int)transform.position.x, tempY = (int)transform.position.z;

        //    Vector3 tempPosition = new Vector3(player.transform.position.x, player.transform.position.z);

        //    pathfinder.GetGrid().GetXY(tempPosition, out x, out y);

        //    //Debug.Log(" End X : " + x);
        //    //Debug.Log(" End Y : " + y);

        //    tempPosition = new Vector3(tempX, tempY);
            
        //    pathfinder.GetGrid().GetXY(tempPosition, out tempX, out tempY);

        //    //Debug.Log(" Start X : " + tempX);
        //    //Debug.Log(" Start X : " + tempY);



        //    List<PathNode> path = pathfinder.FindPath(tempX, tempY, x, y);

        //    //foreach (PathNode node in path)
        //    //{
        //    //    Debug.Log(node.ToString());
        //    //}

        //    if (path == null)
        //        Debug.Log("path e nulo");

        //    if(path != null)
        //        pathPositions = pathfinder.FindPathPositionsOnMap(path);
            
        //    isWalkingTowardsPlayer = true;

        //    if(path != null)
        //    {
        //        for(int i = 0; i < path.Count - 1; i++)
        //        {
                    

        //            //Debug.DrawLine(new Vector3(path[i].x, path[i].y) * cellSize + new Vector3(1, 1, 0) * cellSize * 0.5f,
        //            //    new Vector3(path[i + 1].x, path[i + 1].y) * cellSize + new Vector3(1, 1, 0) * cellSize * 0.5f,
        //            //    Color.red, 10f);


        //            Debug.DrawLine(new Vector3(path[i].x, 0, path[i].y) * cellSize + new Vector3(1, 0, 1) * cellSize * 0.5f,
        //                new Vector3(path[i + 1].x, 0, path[i + 1].y) * cellSize + new Vector3(1, 0, 1) * cellSize * 0.5f,
        //                Color.red, 10f);

        //            //Debug.DrawLine()
        //        }
        //    }
        //}

        //if (Input.GetMouseButtonUp(1))
        //{

        //    Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        //    pathfinder.GetGrid().GetXY(mouseWorldPosition, out x, out y);

        //    pathfinder.GetNode(x, y).SetIsWalkable(!pathfinder.GetNode(x, y).isWalkable);

        //}

        //if (isWalkingTowardsPlayer)
        //{
        //    Debug.Log("Walking towards player");

        //    //if (enemyPathIndex == width * height)
        //    //{
        //    //    isWalkingTowardsPlayer = false;
        //    //    enemyPathIndex = 0;

        //    //    return;
        //    //}

        //    Debug.Log("Distancia : " + Vector3.Distance(transform.position, player.transform.position));

        //    if (Vector3.Distance(transform.position, player.transform.position) <= 10f || enemyPathIndex == pathPositions.Count)
        //    {
        //        isWalkingTowardsPlayer = false;
        //        enemyPathIndex = 0;

        //        return;
        //    }

        //    if (isWalkingTowardsPlayer)
        //    {
        //        //Debug.Log("Index : " + enemyPathIndex);
        //        //Debug.Log("Position: " + pathPositions[enemyPathIndex]);

        //        float step = speed * Time.deltaTime;
        //        Debug.Log("Index : " + enemyPathIndex);
        //        transform.position = Vector3.MoveTowards(transform.position, pathPositions[enemyPathIndex], step);

        //        if(Vector3.Distance(transform.position, pathPositions[enemyPathIndex]) <= 0.5f)
        //        {
        //            //transform.position = pathPositions[enemyPathIndex];
        //            enemyPathIndex++;
        //        }

                

                
        //    }
        //}
    }
}
