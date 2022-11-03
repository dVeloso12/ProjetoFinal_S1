using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRun : MonoBehaviour
{
    [SerializeField] GameObject enemiesManagerPrefab;

    [SerializeField] public List<GameObject> ListStages;
    List<GameObject> UseStage;
    [SerializeField]int maxNStages;
    [SerializeField] GameObject BossRoom;
    [SerializeField] GameObject StageStart;
    public GameObject saveBossStage;
    public bool doUrJob;


    List<GameObject> stagesOrder;
    List<Vector3> stageStartPositions;

    int currentStage;

    public static GenerateRun instance;
    GameObject enemiesManagerInstantiated;


    public GameObject EnemiesManagerInstantiated
    {
        get { return enemiesManagerInstantiated; }
    }
    public List<GameObject> StagesOrder
    {
        get { return stagesOrder; }
    }
    public List<Vector3> StageStartPositions
    {
        get { return stageStartPositions; }
    }

    private void Awake()
    {
        instance = this;
        enemiesManagerInstantiated = Instantiate(enemiesManagerPrefab);
    }

    void Start()
    {
        currentStage = 0;
        saveStagesList();
        Generate();
    }
    void saveStagesList()
    {
        UseStage = new List<GameObject>(ListStages);

        stageStartPositions = new List<Vector3>();
        stagesOrder = new List<GameObject>();
    }
     public GameObject getBossRoom()
    {
        return saveBossStage;
    }
    void Generate()
    {
        var st0 = Instantiate(StageStart, Vector3.zero, Quaternion.identity);
        st0.transform.parent = this.transform;
        Vector3 nextPosition = Vector3.zero;

        stageStartPositions.Add(nextPosition);

        for (int i = 0; i < maxNStages; i++)
        {
            int ran = Random.Range(0, UseStage.Count);
            var st1 = Instantiate(UseStage[ran], nextPosition, Quaternion.identity);
            st1.transform.parent = this.transform;
            nextPosition += UseStage[ran].GetComponent<StageInfos>().StageSize;

            stagesOrder.Add(st1);
            stageStartPositions.Add(nextPosition);

            UseStage.RemoveAt(ran);
        }

        var bossStg = Instantiate(BossRoom, nextPosition,Quaternion.identity);
        bossStg.transform.parent = this.transform;
        saveBossStage = bossStg;
        doUrJob = true;

        stagesOrder.Add(bossStg);
    }

    private int Choose_Stage(List<GameObject> ToUseStages)
    {
        int[] Numbers = SaveStagesInNumbers(ToUseStages);
        var index = Random.Range(0, Numbers.Length);
        return Numbers[index];
    }

    private int[] SaveStagesInNumbers(List<GameObject> ToUseStages)
    {
        int[] stageNumbers = new int[ToUseStages.Count];
        for (int i = 0; i < ToUseStages.Count; i++)
        {
            stageNumbers[i] = ToUseStages[i].GetComponent<StageInfos>().Stage_Id;
        }

        return stageNumbers;
    }

    private void ResetMapGrid()
    {

        Vector3 pos = stageStartPositions[currentStage];
        GameObject stage = stagesOrder[currentStage];

        GameObject groundCheck = null;

        foreach (Transform obj in stage.GetComponentsInChildren<Transform>())
        {
            if(obj.name == "GroundCheck")
            {
                groundCheck = obj.gameObject;
            }
        }

        if(groundCheck == null)
        {
            Debug.Log("Ground check e nulo. (Ao dar reset da grid)");
            return;
        }



        NodeGroundCheck tempScript = groundCheck.GetComponent<NodeGroundCheck>();
        MapGrid<PathNode> tempGrid = Pathfinder.Instance.GetGrid();

        Vector3 finalGridPosition = pos + tempScript.gridOriginPositionOffset;

        float cellSize = tempScript.cellSize;

        int width = tempScript.mapWidth / (int)cellSize
            , height = tempScript.mapHeight / (int)cellSize;


        Debug.Log("Final Grid Position : " + finalGridPosition);

        Pathfinder.Instance.GetGrid().ResetGrid(width, height, cellSize, finalGridPosition, groundCheck, (MapGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));

        

    }

    public void PassedDoor()
    {
        currentStage++;
        Debug.Log("Current Stage : " + currentStage);
        ResetMapGrid();

        enemiesManagerInstantiated.GetComponent<Spawn>().StartOvertTimeSpawning();

    }

}
    
