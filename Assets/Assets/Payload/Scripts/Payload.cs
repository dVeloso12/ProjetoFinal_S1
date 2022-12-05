using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PayloadState { Iddle,IddleToMove,onMoving,onRotate,Stopped,MoveToIddle}
public class Payload : MonoBehaviour
{
    const float ROTATION_SPEED_OFFSET = 20F;
    [Header("Payload Plates")]
    public float speed;
    public bool Ended;
    public bool canMove;
    public List<Transform> ListPlates;

    [Header("For Debug")]
    [SerializeField] PayloadState state;
    [SerializeField] StageSpawner spwaner;
    public short index;
    public int maxIndex; 
    bool canIddle;
    float animTimer;
    bool startTimerAnim;    
    Animation animator;   
    bool forceRotate;
    bool ReaperAttempt=true;

    void Start()
    {
        animator = GetComponent<Animation>();
        maxIndex = ListPlates.Count;
        state = PayloadState.Iddle;
    }

    void Update()
    {
        if (index + 1 == maxIndex&&ReaperAttempt)
        {
            spwaner.ReaperSpawn();
            ReaperAttempt = false;
        }
        MovePayload();
        PlayAnimations();
        SetSomeOptions();
    }

    #region Payload Functions

    void SetSomeOptions()
    {
        if(Ended)
        {
            GetComponent<Collider>().enabled = false;
        }
    }
    void PlayAnimations()
    {
        if (canMove)
        {
            animator.Play("IddleToMoveMode");
            canMove = false;
            startTimerAnim = true;
            state = PayloadState.IddleToMove;

        }
        if (canIddle)
        {
            animator.Play("MoveToIddleMode");
            canIddle = false;

        }
        if (startTimerAnim)
        {
            animTimer += Time.deltaTime;
            if (state == PayloadState.IddleToMove && animTimer >= 5f)
            {
                state = PayloadState.onMoving;
                animTimer = 0;
                startTimerAnim = false;
            }
            if(animTimer >= 5f  && state == PayloadState.MoveToIddle)
            {
                Ended = true;
                startTimerAnim = false;
                animTimer = 0;
            }

        }
    }

    void MovePayload()
    {
        if (!Ended)
        {
            
            if (index+1 <= maxIndex)
            {
                if (state == PayloadState.onRotate || forceRotate)
                {
                    if (transform.localRotation != ListPlates[index].localRotation)
                    {
                        RotateToPlate(ListPlates[index]);
                    }
                    else if (transform.localRotation == ListPlates[index].localRotation)
                    {
                        state = PayloadState.Stopped;
                        forceRotate = false;
                    }
                }
            }
            else
            {
                canIddle = true;
                state = PayloadState.MoveToIddle;
                startTimerAnim = true;
            }


            if (state == PayloadState.onMoving && !forceRotate)
            {
                if (transform.localPosition != ListPlates[index].localPosition)
                {
                    MoveToPlate(ListPlates[index]);
                }
                else if (transform.localPosition == ListPlates[index].localPosition)
                {

                    state = PayloadState.onRotate;
                    index++;
                    forceRotate = true;
                }
            }
        }
     
    }

    void MoveToPlate(Transform plate)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, plate.localPosition, Time.deltaTime*speed);
    }
    void RotateToPlate(Transform plate)
    {
        transform.rotation = Quaternion.RotateTowards(transform.localRotation, plate.localRotation, Time.deltaTime * speed * ROTATION_SPEED_OFFSET);

    }
#endregion
    #region Payload Detection
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "Player")
        {
            Debug.Log(state);
            if(state == PayloadState.Iddle)
            {
              canMove = true;
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == "Player")
        {
            if (state != PayloadState.IddleToMove) state = PayloadState.onMoving;
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Player")
        {         
                state = PayloadState.Stopped;
     
        }
    }

    #endregion
}
