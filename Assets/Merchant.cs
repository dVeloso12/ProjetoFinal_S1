using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    public bool isTutorial;
    NpcMsgDisplay npc;
    AudioSource audio;
    bool doAudio;


    // Start is called before the first frame update
    void Start()
    {
        if(isTutorial) npc = FindObjectOfType<NpcMsgDisplay>();
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        SoundManager();
    }

    void SoundManager()
    {
       if(doAudio)
        {
            audio.Play();
            doAudio = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player" && Input.GetKey(KeyCode.H))
        {
            if(isTutorial && npc.canInteract)
            {
                GameManager gm = FindObjectOfType<GameManager>();
                gm.gameState = 1;
                gm.AddUpgrade();
            }
            else if(!isTutorial)
            {
                GameManager gm = FindObjectOfType<GameManager>();
                gm.gameState = 1;
                gm.AddUpgrade();
            }
                
        }
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            doAudio = true;
        }
    }

}
