using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayOrganize : MonoBehaviour
{
    GameObject saveStages,savelobby,saveshop;
    [Header("Player Stuff")]
    [SerializeField] GameObject Player;
    [Header("Stages Stuff")]
    [SerializeField] GameObject StageRender;
    [SerializeField] Vector3 PlayerStageSpawn;
    [Header("Looby Stuff")]
    [SerializeField] GameObject Looby;
    [SerializeField] Vector3 LobbySpawnPos;
    [SerializeField] Vector3 PlayerLobbySpawn;
    [Header("Shop Stuff")]
    [SerializeField] GameObject Shop;
    [SerializeField] Vector3 ShopSpawnPos;
    [SerializeField] Vector3 PlayerShopSpawn;
    [Header("Gameplay")]
    public bool toLobby;
    public bool toGame;
    public bool goToStage;
    public bool goToShop;
    public GameObject playerIns;


    void Start()
    {
        saveStages = null;
        playerIns = Instantiate(Player, PlayerLobbySpawn, Quaternion.identity);
        //Player.transform.position = PlayerLobbySpawn;
        Generate_Delete_Looby(true, false);
    }

    void Update()
    {
        if(toGame)
        {
            Generate_Detele_Stages(true, false);
            Generate_Delete_Shop(true, false);
            Generate_Delete_Looby(false, true);
            goToStage = true;
            toGame = false;
        }
        if(toLobby)
        {
            Generate_Detele_Stages(false, true);
            Generate_Delete_Shop(false, true);
            Generate_Delete_Looby(true, false);
            PlayerMove(PlayerLobbySpawn);
            toLobby = false;
        }
        if(goToStage)
        {
            PlayerMove(PlayerStageSpawn);
            goToStage = false;
        }
        if(goToShop)
        {
            PlayerMove(PlayerShopSpawn);
            goToShop = false;
        }
    }
    void PlayerMove(Vector3 newPos)
    {
        Destroy(playerIns);
        playerIns = null;
        playerIns = Instantiate(Player, newPos, Quaternion.identity);
        //playerIns.transform.position = newPos;
    }
    void Generate_Detele_Stages(bool canGenerateStages,bool canDeleteStages)
    {

        if(canGenerateStages)
        {
            saveStages = Instantiate(StageRender, Vector3.zero, Quaternion.identity);
            canGenerateStages = false;
        }
        if(canDeleteStages)
        {
            Destroy(saveStages);
            saveStages = null;
            canDeleteStages = false;
        }

    }
    void Generate_Delete_Looby(bool canGenerateLobby, bool canDeleteLobby)
    {
        if(canGenerateLobby)
        {
            savelobby = Instantiate(Looby, LobbySpawnPos, Quaternion.identity);
            canGenerateLobby = false;
        }
        if(canDeleteLobby)
        {
            Destroy(savelobby);
            savelobby = null;
            canDeleteLobby = false;
        }
    }
    void Generate_Delete_Shop(bool canGenerateShop, bool canDeleteShop)
    {
        if(canGenerateShop)
        {
            saveshop = Instantiate(Shop, ShopSpawnPos, Quaternion.identity);
            canGenerateShop = false;
        }
        if(canDeleteShop)
        {
            Destroy(saveshop);
            saveshop = null;
            canDeleteShop = false;
        }
    }
}
