using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour
{
    [Header("Animations")]
    Animation animator;
    public bool canMove;
    public bool canIddle;


    void Start()
    {
        animator = GetComponent<Animation>();
    }

    void Update()
    {
        PlayAnimations();
    }

    void PlayAnimations()
    {
        if (canMove)
        {
            animator.Play("IddleToMoveMode");
            canMove = false;
        }
        if (canIddle)
        {
            animator.Play("MoveToIddleMode");
            canIddle = false;

        }
    }
}
