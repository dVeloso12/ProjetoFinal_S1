using UnityEngine;

public class EndBossPortal : MonoBehaviour
{
     GameplayOrganize game;
    public bool isTutorial;
    GameManager gm;
    NpcMsgDisplay npc;
    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameplayOrganize>();
        gm = GameObject.FindObjectOfType<GameManager>();
        npc = GameObject.FindObjectOfType<NpcMsgDisplay>();
       
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.name == "Player")
        {
            if( !isTutorial )
            {
                gm.DifficultyMod += .3f;
                if (gm.ReaperSpawn < 50)
                    gm.ReaperSpawn += 5;
                game.goToShop = true;
                if (!isTutorial)
                    game.PlayerProperty.GetComponent<HealingItem>().ResetCount();
            }
            else if( isTutorial && npc.canTP)
            {
                game.goToShop = true;
            }
           

        }
    }
  

}
