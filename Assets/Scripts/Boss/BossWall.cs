using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour
{
    [Header("Plates")]
    [SerializeField] GameObject PlateLeft;
    [SerializeField] GameObject PlateRight;
    [Header("Wall")]
    [SerializeField] GameObject Wall;
    [SerializeField] Material matWall;
    [Header("General Stuff")]
    float timerWall;
    public float speed;
    public bool WallUp;
    public bool canIncrease;


    void Start()
    {
        matWall.SetFloat("_Fill", 1f);
        WallUp = true;
        canIncrease = false;
        timerWall = matWall.GetFloat("_Fill");
    }

    void Update()
    {

        if (PlateLeft.GetComponent<PlateBossRoom>().plateReady && PlateRight.GetComponent<PlateBossRoom>().plateReady && WallUp)
        {
            DecreaseWall();     
        }
        if(canIncrease)
        {
            IncreaseWall();
        }
    }
    void DecreaseWall()
    {
        timerWall -= Time.deltaTime * speed;
        matWall.SetFloat("_Fill", timerWall);
        if(timerWall <= -0.5f)
        {
            timerWall = -0.5f;
            this.GetComponent<BoxCollider>().enabled = false;
            WallUp = false;

        }
    }
    void IncreaseWall()
    {
        timerWall += Time.deltaTime * speed;
        matWall.SetFloat("_Fill", timerWall);
        if (timerWall >= 1f)
        {
            timerWall = 1f;
            WallUp = true;
            canIncrease = false;
            this.GetComponent<BoxCollider>().enabled = true;
            PlateLeft.GetComponent<PlateBossRoom>()._resetValues = true;
            PlateRight.GetComponent<PlateBossRoom>()._resetValues = true;
        }
    }
}
