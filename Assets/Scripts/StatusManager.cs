using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    GameManager gameManager;
    Animation anim;
    public bool doingAnim;
    void Start()
    {
        doingAnim = false;
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.statsOpen && doingAnim)
        {
            anim.Play("ShowPlayerStatus");
            doingAnim = false;
        }
        else if(!gameManager.statsOpen && doingAnim)
        {
            anim.Play("HidePlayerStatus");
            doingAnim = false;
        }
    }
}
