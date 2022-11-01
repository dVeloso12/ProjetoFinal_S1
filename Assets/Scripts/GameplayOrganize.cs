using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayOrganize : MonoBehaviour
{
   public GameObject saveStages,savelobby,saveshop;
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
    [SerializeField] public Vector3 PlayerShopSpawn;
    [Header("Gameplay")]
    public bool toLobby;
    public bool toGame;
    public bool goToStage;
    public bool goToShop;
    public GameObject playerIns;
    public GameObject saveBossRoom;
    bool saved;

    public static GameplayOrganize instance;

    private void Awake()
    {
        instance = this;
        playerIns = Instantiate(Player, PlayerLobbySpawn, Quaternion.identity);

    }
    void Start()
    {
        saveStages = null;
        //playerIns = Instantiate(Player, PlayerLobbySpawn, Quaternion.identity);
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
            GoToStage();
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
        if(goToShop)
        {
            GoToShop();
            goToShop = false;
        }
        if(saveStages.GetComponent<GenerateRun>().doUrJob)
        {
            saveBossRoom = saveStages.GetComponent<GenerateRun>().getBossRoom();
            saveStages.GetComponent<GenerateRun>().doUrJob = false;
            saved = true;
        }
        if(saved)
        {
            if (saveBossRoom.GetComponent<BossStageInfo>().getCollidePortal() == true)
            {
                Debug.Log("AQUI BOSS-GameORG");
                GoToShop();
            }
        }
        if (saveshop.GetComponent<SaveInfosShop>().getCollideButton() == true)
        {
            ResetGenerator();
            saveshop.GetComponent<SaveInfosShop>().setCollideButton(false);
        }
        if (saveshop.GetComponent<SaveInfosShop>().getCollidePortal() == true)
        {
            Debug.Log("ENTREI - 1");
            GotoStageFromShop();
            saveshop.GetComponent<SaveInfosShop>().setCollidePortal(false);

        }
    }

    void ResetGenerator()
    {
        Generate_Detele_Stages(false, true);
        Generate_Detele_Stages(true, false);
    }
    public void GoToShop()
    {
        PlayerMove(PlayerShopSpawn);
    }

    public void GoToStage()
    {
        PlayerMove(PlayerStageSpawn);    
    }

    public void GotoStageFromShop()
    {
        PlayerMove(PlayerStageSpawn);
    }

    void PlayerMove(Vector3 newPos)
    {
        playerIns.GetComponentInChildren<PlayerMovement>().transform.position = newPos;
        Debug.Log("ENTREI - 2");
        Physics.SyncTransforms();
    }
    void Generate_Detele_Stages(bool canGenerateStages,bool canDeleteStages)
    {

        if(canGenerateStages)
        {
            saveStages = Instantiate(StageRender, Vector3.zero, Quaternion.identity);
            Debug.Log("New Stage");
            canGenerateStages = false;
           
        }
        if(canDeleteStages)
        {
            Destroy(saveStages);
            Debug.Log("Deleted Stage");
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
