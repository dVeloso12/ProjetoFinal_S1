using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PayloadScript : MonoBehaviour
{
    [SerializeField] PathCreator path;
    [SerializeField] float speed = 5f;
    float distanceTravelled;
    [SerializeField] public bool canMove;
    public float EndPoint;
    public bool PayloadCompleted;


   
    void Update()
    {
        if (canMove)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = path.path.GetRotationAtDistance(distanceTravelled);

        }
        
    }
}
