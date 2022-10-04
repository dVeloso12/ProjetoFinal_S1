using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PayloadScript : MonoBehaviour
{
    [SerializeField] PathCreator path;
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject Stage;
    float distanceTravelled;
    [SerializeField] public bool canMove;
    public float EndPoint;
    public bool PayloadCompleted;
    //x = 262.523 ;
    private void Start()
    {
        EndPoint += Stage.transform.position.x;
    }

    void Update()
    {
        if (canMove)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = path.path.GetRotationAtDistance(distanceTravelled);

            if (transform.position.x >= EndPoint)
            {
                canMove = false;
                PayloadCompleted = true;
            }
        }

        if(PayloadCompleted && !canMove)
        {
           
        }
        
    }
}
