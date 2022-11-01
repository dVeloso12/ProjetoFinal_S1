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

    //[SerializeField] PathfindingVisual visual;
    /*[SerializeField] */GameObject groundCheck;
    [SerializeField] GameObject player;
    [SerializeField] Vector3 initialPosition;


    private Pathfinder pathfinder;
    private EnemyManager enemyManager;

    //GameObject instantiatedPlayer;

    List<Vector3> pathPositions;

    // Start is called before the first frame update
    void Start()
    {

        GenerateRun generator = GenerateRun.instance;

        if (generator == null) Debug.LogWarning("Generator e nulo");

        Transform[] tempList = generator.StagesOrder[0].GetComponentsInChildren<Transform>();

        foreach(Transform obj in tempList)
        {
            Debug.Log("Stage nane : " + obj.name);

            if(obj.name == "GroundCheck")
            {
                groundCheck = obj.gameObject;
            }

        }

        if(groundCheck == null)
        {
            Debug.Log("Ground check e nulo");
            return;
        }

        NodeGroundCheck tempScript = groundCheck.GetComponent<NodeGroundCheck>();

        Debug.Log("Width : " + tempScript.mapWidth);
        Debug.Log("Height : " + tempScript.mapHeight);

        width = tempScript.mapWidth / (int)cellSize;
        height = tempScript.mapHeight / (int)cellSize;

        originPosition = tempScript.gridOriginPositionOffset + generator.StageStartPositions[0];

        Debug.Log("Stage Position : " + generator.StageStartPositions[0]);
        Debug.Log("Grid Starting Position : " + tempScript.gridOriginPositionOffset);

        Debug.Log("Combined : " + originPosition);

        //instantiatedPlayer = Instantiate(player, initialPosition, Quaternion.identity);

        pathfinder = new Pathfinder(width, height, cellSize, groundCheck, raycastHeight, originPosition, groundLayerMask);
        enemyManager = new EnemyManager(player);

        //visual.SetGrid(pathfinder.GetGrid());
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
