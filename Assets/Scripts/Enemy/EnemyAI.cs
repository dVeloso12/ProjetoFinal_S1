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

    [SerializeField] bool isRigidbody;

    Pathfinder pathfinderInstance;

    Vector3 previousPlayerPosition;
    List<Vector3> pathPositionsList;

    bool isWalkingTowardsPlayer, pathImpeded;
    float movementSpeed;
    int enemyPositionIndex;

    [Header("Rigidbody")]
    [SerializeField] float movementForce = 200f;

    Rigidbody enemyRB;

    [Header("Shooting")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletsParent;

    [SerializeField] float fireRate = 60f;


    float shootingTimer;

    private void Awake()
    {
        if(isRigidbody)
            enemyRB = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pathfinderInstance = Pathfinder.Instance;

        pathPositionsList = new List<Vector3>();

        movementSpeed = defaultMovementSpeed;

        previousPlayerPosition = new Vector3(-100, -100, -100);

        isWalkingTowardsPlayer = false;
        pathImpeded = false;
        enemyPositionIndex = 0;
        shootingTimer = 0f;
    }

    public void SetPlayerObject(GameObject playerObject)
    {
        Debug.Log("Setting Player");
        //PlayerObject = playerObject;

        foreach(Transform temp in playerObject.GetComponentsInChildren<Transform>())
        {
            if(temp.tag == "Player")
            {
                PlayerObject = temp.gameObject;
            }
        }
        Debug.Log("Player : " + playerObject);
    }

    // Update is called once per frame
    void Update()
    {
        shootingTimer += Time.deltaTime;
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

            if(Vector3.Distance(transform.position, PlayerObject.transform.position) <= gunRange * 2)
            {
                CheckPlayerInSight();
            }


        }

    }

    public void CheckIfPlayerMoved()
    {
        if (previousPlayerPosition == new Vector3(-100, -100, -100) )
        {
            Debug.LogWarning("Primeiro movimento");
            FindPathToPlayer();
            previousPlayerPosition = PlayerObject.transform.position;

        }
        else if (previousPlayerPosition != PlayerObject.transform.position)
        {
            Debug.LogWarning("Jogador moveu se");
            FindPathToPlayer();
            previousPlayerPosition = PlayerObject.transform.position;
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

        if (tempPath == null)
        {
            Debug.Log("Path e nulo");
            return;
        }

        pathPositionsList = pathfinderInstance.FindPathPositionsOnMap(tempPath);

        previousPlayerPosition = PlayerObject.transform.position;

        if (pathPositionsList == null) Debug.Log("Path  Positions e nulo");

    }
    
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
                Shoot(directionToLook);

                if(angleDiference > .5f || angleDiference < -.5f)
                {

                    transform.LookAt(PlayerObject.transform);

                }

                transform.LookAt(hit.transform);

            }


        }
        else
        {
            isWalkingTowardsPlayer = true;
        }

        Debug.Log("IS player in sight : " + isPlayerInSight);
        Debug.Log("Name : " + gameObject.name);

        return isPlayerInSight;
    }
   
    public void Shoot(Vector3 directionToLook)
    {

        if(shootingTimer > 1f / fireRate)
        {
            //Shooting
            Debug.Log("Shooting");
            //Debug.DrawLine(transform.position, transform.position + directionToLook, Color.red, 1f);

            bulletsParent.transform.LookAt(PlayerObject.transform);

            Quaternion directionToShoot = new Quaternion(directionToLook.x, directionToLook.y, directionToLook.z, 1f);

            Instantiate(bulletPrefab, bulletsParent.transform.position, bulletsParent.transform.rotation, bulletsParent.transform);

            shootingTimer = 0f;
        }


    }

    public void FollowPlayer()
    {
        float step = movementSpeed * Time.deltaTime;

        if (CheckPlayerInSight() || enemyPositionIndex >= pathPositionsList.Count/* || pathImpeded*/)
        {
            enemyPositionIndex = 0;
            isWalkingTowardsPlayer = false;
            return;
        }

        if (enemyPositionIndex == 0)
            enemyPositionIndex++;

        if(enemyPositionIndex >= pathPositionsList.Count)
        {
            enemyPositionIndex = pathPositionsList.Count - 1;
            isWalkingTowardsPlayer = false;
            return;
        }

        Vector3 directionToMove = (pathPositionsList[enemyPositionIndex] - transform.position);
        Vector3 directionToMoveNormalized = directionToMove.normalized;

        //Debug.DrawLine(transform.position, transform.position + directionToMoveNormalized * directionToMove.magnitude, Color.green, 10f);

        transform.LookAt(pathPositionsList[enemyPositionIndex]);

       
        if (!isRigidbody)
            transform.position += directionToMoveNormalized * step;
        else
        {
            enemyRB.AddForce(movementForce * Time.deltaTime * transform.forward);
        }

        if (Vector3.Distance(transform.position, pathPositionsList[enemyPositionIndex]) <= .5f)
        {

            transform.position = pathPositionsList[enemyPositionIndex];

            enemyPositionIndex++;

        }

       

    }

    

    
}
