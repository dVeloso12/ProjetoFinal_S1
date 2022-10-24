using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    enum EnemySearchMode { AlwaysSearchingMode, WithinRangeSearchingMode }


    [SerializeField] EnemySearchMode enemySearchMode;
    [SerializeField] GameObject PlayerObject;
    

    [SerializeField] float gunRange;
    [SerializeField] float enemyFOV;
    [SerializeField] float turnSpeed, movementSpeed;

    Pathfinder pathfinderInstance;

    Vector3 previousPlayerPosition;
    List<Vector3> pathPositionsList;

    bool isWalkingTowardsPlayer;

    int enemyPositionIndex;

    // Start is called before the first frame update
    void Start()
    {
        pathfinderInstance = Pathfinder.Instance;

        pathPositionsList = new List<Vector3>();


        isWalkingTowardsPlayer = false;

        enemyPositionIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemySearchMode == EnemySearchMode.AlwaysSearchingMode)
        {

            CheckIfPlayerMoved();
            isWalkingTowardsPlayer = true;

            if(Vector3.Distance(transform.position, PlayerObject.transform.position) >= gunRange)
            {

                FollowPlayer();

            }
            else
            {

                LookTowardsPlayer();

            }

        }
        else
        {

            CheckIfPlayerMoved();

            if(Vector3.Distance(transform.position, PlayerObject.transform.position) < gunRange)
            {
                LookTowardsPlayer();
            }


        }
        


    }
    
    public void CheckIfPlayerMoved()
    {
        if (previousPlayerPosition == null)
        {

            FindPathToPlayer();
            previousPlayerPosition = PlayerObject.transform.position;

        }
        else if (previousPlayerPosition != PlayerObject.transform.position)
        {

            FindPathToPlayer();
            previousPlayerPosition = PlayerObject.transform.position;

        }
    }

    public void FindPathToPlayer()
    {

        int startX, startY;
        int endX, endY;

        Vector3 tempPos = new Vector3(transform.position.x, transform.position.z);

        pathfinderInstance.GetGrid().GetXY(tempPos, out startX, out startY);

        tempPos = new Vector3(PlayerObject.transform.position.x, PlayerObject.transform.position.z);

        pathfinderInstance.GetGrid().GetXY(tempPos, out endX, out endY);

        Debug.Log("Start X = " + startX);
        Debug.Log("End Pos = " + endX + " , " + endY);

        List<PathNode> tempPath;

        if (pathfinderInstance == null) Debug.Log("Pathfinder e nulo");

        tempPath = pathfinderInstance.FindPath(startX, startY, endX, endY);

        if (tempPath == null) Debug.Log("Path e nulo");

        pathPositionsList = pathfinderInstance.FindPathPositionsOnMap(tempPath);

        if (pathPositionsList == null) Debug.Log("Path  Positions e nulo");

    }

    public void LookTowardsPlayer()
    {

        Vector3 directionToLook = (PlayerObject.transform.position - transform.position).normalized;

        float angleDiference = Vector3.Angle(directionToLook, transform.forward);


        Debug.DrawLine(transform.position, transform.position + directionToLook * gunRange);

        if (angleDiference <= 1f || angleDiference >= -1f)
        {

            Shoot();

        }
        else if (Vector3.Angle(transform.forward, directionToLook) < enemyFOV)
        {

            transform.Rotate(Vector3.up * angleDiference * turnSpeed * Time.deltaTime);

        }


    }
        

    

    public bool Aim()
    {

        RaycastHit hitInfo;
        Vector3 target = PlayerObject.transform.position;

        transform.LookAt(target);

        if(Physics.Raycast(transform.position, transform.forward, out hitInfo))
        {

            return true;
        }
        else
        {
            return false;
        }

    }

    public void Shoot()
    {

        //Shooting
        Debug.Log("Shooting");
    }

    public void FollowPlayer()
    {
        float step = movementSpeed * Time.deltaTime;

        Vector3 directionToMove = (pathPositionsList[enemyPositionIndex] - transform.position).normalized;

        transform.position += directionToMove * step;

        if (Vector3.Distance(transform.position, pathPositionsList[enemyPositionIndex]) <= .5f)
        {

            transform.position = pathPositionsList[enemyPositionIndex];

            enemyPositionIndex++;

        }

    }

}
