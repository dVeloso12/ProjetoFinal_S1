using UnityEngine;

public class EndBossPortal : MonoBehaviour
{
     GameplayOrganize game;
    public bool isTutorial;
    GameManager gm;
    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameplayOrganize>();
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.name == "Player")
        {
            gm.DifficultyMod += .3f;
            if (gm.ReaperSpawn < 50)
                gm.ReaperSpawn += 5;
            game.goToShop = true;
            game.PlayerProperty.GetComponent<HealingItem>().ResetCount();

        }
    }
  

}
