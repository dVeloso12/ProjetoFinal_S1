using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    enum EnemySearchMode { AlwaysSearchingMode, WithinRangeSearchingMode }


    [SerializeField] EnemySearchMode enemySearchMode;
    GameObject PlayerObject;


    [SerializeField] float gunRange, sightRange;
    [SerializeField] float enemyFOV;
    [SerializeField] float turnSpeed, defaultMovementSpeed;

    Pathfinder pathfinderInstance;

    Vector3 previousPlayerPosition;
    List<Vector3> pathPositionsList;

    bool isWalkingTowardsPlayer;
    float movementSpeed;
    int enemyPositionIndex;

    // Start is called before the first frame update
    void Start()
    {
        pathfinderInstance = Pathfinder.Instance;

        pathPositionsList = new List<Vector3>();

        movementSpeed = defaultMovementSpeed;

        previousPlayerPosition = new Vector3(-100, -100, -100);

        isWalkingTowardsPlayer = false;

        enemyPositionIndex = 0;
    }

    public void SetPlayerObject(GameObject playerObject)
    {
        Debug.Log("Setting Player");
        PlayerObject = playerObject;
        Debug.Log("Player : " + playerObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerObject == null) Debug.LogWarning("Player e nulo");

        if (enemySearchMode == EnemySearchMode.AlwaysSearchingMode)
        {

            if (!CheckPlayerInSight())
            {

                CheckIfPlayerMoved();
                FollowPlayer();

            }

        }
        else if (enemySearchMode == EnemySearchMode.WithinRangeSearchingMode)
        {



        }

    }

    public void CheckIfPlayerMoved()
    {
        if (previousPlayerPosition == new Vector3(-100, -100, -100) )
        {
            Debug.LogWarning("Primeiro movimento");
            FindPathToPlayer();
            //previousPlayerPosition = PlayerObject.transform.position;

        }
        else if (previousPlayerPosition != PlayerObject.transform.position)
        {
            Debug.LogWarning("Jogador moveu se");
            FindPathToPlayer();
            //previousPlayerPosition = PlayerObject.transform.position;

        }
        //else
        //{
        //    Debug.Log("Previous Position : " + previousPlayerPosition);
        //    Debug.Log("Player Positionm : " + PlayerObject.transform.position);
        //}
    }
    
    public void FindPathToPlayer()
    {

        int startX, startY;
        int endX, endY;

        Vector3 tempPos = new Vector3(transform.position.x, transform.position.z);

        pathfinderInstance.GetGrid().GetXY(tempPos, out startX, out startY);

        tempPos = new Vector3(PlayerObject.transform.position.x, PlayerObject.transform.position.z);

        pathfinderInstance.GetGrid().GetXY(tempPos, out endX, out endY);

        //Debug.Log("Start X = " + startX);
        //Debug.Log("End Pos = " + endX + " , " + endY);

        List<PathNode> tempPath;

        if (pathfinderInstance == null) Debug.Log("Pathfinder e nulo");

        tempPath = pathfinderInstance.FindPath(startX, startY, endX, endY);

        if (tempPath == null) Debug.Log("Path e nulo");

        pathPositionsList = pathfinderInstance.FindPathPositionsOnMap(tempPath);

        previousPlayerPosition = PlayerObject.transform.position;

        if (pathPositionsList == null) Debug.Log("Path  Positions e nulo");

    }

    //public void LookTowardsPlayer()
    //{

    //    Vector3 directionToLook = (PlayerObject.transform.position - transform.position).normalized;

    //    float angleDiference = Vector3.Angle(directionToLook, transform.forward);



    //    if (angleDiference <= 1f || angleDiference >= -1f)
    //    {

    //        if (Aim(directionToLook))
    //        {
    //            Debug.DrawLine(transform.position, transform.position + directionToLook * gunRange);
    //            Shoot();
    //        }
            

    //    }
    //    else if (Vector3.Angle(transform.forward, directionToLook) < enemyFOV)
    //    {

    //        transform.Rotate(Vector3.up * angleDiference * turnSpeed * Time.deltaTime);

    //    }


    //}
        
    public bool CheckPlayerInSight()
    {

        bool isPlayerInSight = false;

        Vector3 directionToLook = (PlayerObject.transform.position - transform.position);
        float angleDiference = Vector3.Angle(directionToLook.normalized, transform.forward);

        RaycastHit hit;

        if(Physics.Raycast(transform.position, directionToLook.normalized, out hit, gunRange))
        {
            if(hit.collider.tag == "Player")
            {
                isPlayerInSight = true;

                Debug.LogWarning("Player is Shot");
                Shoot();

                Debug.DrawLine(transform.position, transform.position + directionToLook, Color.red, 1f);


                if(angleDiference > .5f || angleDiference < -.5f)
                {

                    transform.LookAt(PlayerObject.transform);

                }

                transform.Rotate(Vector3.up * angleDiference * turnSpeed * Time.deltaTime);

            }


        }
        else
        {
            isWalkingTowardsPlayer = true;
        }

        return isPlayerInSight;
    }
   
    public void Shoot()
    {

        //Shooting
        Debug.Log("Shooting");
    }

    public void FollowPlayer()
    {
        float step = movementSpeed * Time.deltaTime;

        if (CheckPlayerInSight() || enemyPositionIndex >= pathPositionsList.Count)
        {
            enemyPositionIndex = 0;
            isWalkingTowardsPlayer = false;
            return;
        }

        if (enemyPositionIndex == 0)
            enemyPositionIndex++;

        Vector3 directionToMove = (pathPositionsList[enemyPositionIndex] - transform.position);
        Vector3 directionToMoveNormalized = directionToMove.normalized;

        transform.LookAt(pathPositionsList[enemyPositionIndex]);

        RaycastHit hit;

        if(Physics.Raycast(transform.position, directionToMoveNormalized, out hit, directionToMove.x * directionToMove.z))
        {

            if (hit.collider.tag == "Enemy")
            {
                Debug.DrawLine(transform.position, transform.position + directionToMoveNormalized * (directionToMove.x * directionToMove.z), Color.green, 0.5f);

                Vector3 tempPosition = RecalculateNextPosition();

                if (tempPosition != transform.position)
                {
                    pathPositionsList[enemyPositionIndex] = tempPosition;
                    if(movementSpeed != defaultMovementSpeed)
                    {
                        movementSpeed *= 3f;
                    }
                }
                else
                {
                    movementSpeed /= 3f;
                }

            }

        }

        transform.position += directionToMoveNormalized * step;

        if (Vector3.Distance(transform.position, pathPositionsList[enemyPositionIndex]) <= .5f)
        {

            transform.position = pathPositionsList[enemyPositionIndex];

            enemyPositionIndex++;

        }

       

    }

    public Vector3 RecalculateNextPosition()
    {

        Vector3 nextPosition = transform.position;

        int tempX, tempY;

        pathfinderInstance.GetGrid().GetXY(pathPositionsList[enemyPositionIndex], out tempX, out tempY);

        List<PathNode> tempNodeList = pathfinderInstance.GetNeighborNodes(pathfinderInstance.GetNode(tempX, tempY));



        foreach(PathNode node in tempNodeList)
        {
            if (!node.isWalkable)
            {
                tempNodeList.Remove(node);
            }
        }

        float leastAngle = -1000f;
        PathNode nodeToSwitchTo = null;

        foreach(PathNode node in tempNodeList)
        {

            Vector3 tempWorldPosition = pathfinderInstance.GetGrid().GetWorldPosition(node.x, node.y);
            Vector3 tempDirection = tempWorldPosition - transform.position;

            float tempAngle = Vector3.Angle(transform.forward, tempDirection);

            if(leastAngle != -1000f)
            {
                if (Mathf.Abs(tempAngle) < Mathf.Abs(leastAngle))
                {
                    leastAngle = tempAngle;
                    nodeToSwitchTo = node;
                }
            }
            else
            {
                tempAngle = leastAngle;
                nodeToSwitchTo = node;
            }

        }

        if(nodeToSwitchTo == null)
        {

            Debug.LogWarning("No different nodes to switch to.");
            nextPosition = transform.position;

        }

        return nextPosition;
    }

    
}
