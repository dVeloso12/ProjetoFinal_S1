using UnityEngine;

public class EndBossPortal : MonoBehaviour
{
     GameplayOrganize game;
    public bool isTutorial;
    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameplayOrganize>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.name == "Player")
        {
            game.goToShop = true;

        }
    }
  

}
