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
    [SerializeField] float turnSpeed;

    Pathfinder pathfinderInstance;

    Vector3 previousPlayerPosition;
    List<Vector3> pathPositionsList;

    bool isWalkingTowardsPlayer;

    // Start is called before the first frame update
    void Start()
    {
        pathfinderInstance = Pathfinder.Instance;

        pathPositionsList = new List<Vector3>();


        isWalkingTowardsPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemySearchMode == EnemySearchMode.AlwaysSearchingMode)
        {

            CheckIfPlayerMoved();
            isWalkingTowardsPlayer = true;

        }
        else
        {

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

        pathfinderInstance.GetGrid().GetXY(transform.position, out startX, out startY);
        pathfinderInstance.GetGrid().GetXY(PlayerObject.transform.position, out endX, out endY);

        List<PathNode> tempPath;

        tempPath = pathfinderInstance.FindPath(startX, startY, endX, endY);

        pathPositionsList = pathfinderInstance.FindPathPositionsOnMap(tempPath);

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



    }

}
