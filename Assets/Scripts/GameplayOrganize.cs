using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameplayOrganize : MonoBehaviour
{   
    [Header("Player Stuff")]
    [SerializeField] GameObject Player;
    [SerializeField] GameObject PlayerUI;
    [Header("Tutorial Stuff")]
    [SerializeField] Vector3 PlayerSpawnTuto;
    [SerializeField] public bool tutorialFinished;
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
    public bool toTutorial;
    bool locked;
    [Header("Tutorial Stuff")]
    [SerializeField] GameObject TutoNpc;
    [Header("Debug Stuff")]
    public GameObject playerIns;
    public GameObject saveBossRoom;
    public GameObject saveStages, savelobby, saveshop,savenpc;
    public static GameplayOrganize instance;
    HookShot graple;
    Granade granade;
    GameObject playerWeapons;
    GameManager gm;


    public GameObject PlayerProperty
    {
        get { return Player; }
    }

    private void Awake()
    {
        instance = this;
        playerIns = Instantiate(Player, PlayerLobbySpawn, Quaternion.identity);
        granade = GameObject.Find("Player").GetComponent<Granade>();
        graple = GameObject.Find("Player").GetComponent<HookShot>();
        playerWeapons = GameObject.Find("GunDirection");
        gm = GameObject.FindObjectOfType<GameManager>();

    }
    void Start()
    {
        saveStages = null;
  
       
    }

    void Update()
    {
        if(toTutorial)
        {
            LoadTutorial();
            PlayerMove(PlayerSpawnTuto);
            setPlayerSettings(false);
            savenpc = Instantiate(TutoNpc);
            toTutorial = false;
        }
        if(tutorialFinished)
        {
            if (savenpc != null) Destroy(savenpc);
            UnloadTutorial();
            //gm.ResetPlayer();
          tutorialFinished = false;
        }    
        if(toGame)
        {
            Generate_Detele_Stages(true, false);
            Generate_Delete_Shop(true, false);
            Generate_Delete_Looby(false, true);
            GoToStage();
            setPlayerSettings(false);
    
            toGame = false;
        }
        if(toLobby)
        {
           
            Generate_Detele_Stages(false, true);
            Generate_Delete_Shop(false, true);
            Generate_Delete_Looby(true, false);
            PlayerMove(PlayerLobbySpawn);
            setPlayerSettings(true);
            toLobby = false;
        }
        if(goToShop)
        {
            GoToShop();
            goToShop = false;
        }
       


    }
    void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene", LoadSceneMode.Additive);
       
    }
   public void UnloadTutorial()
    {
        SceneManager.UnloadScene("TutorialScene");
    }

    void setPlayerSettings(bool isInLobby)
    {
        if(isInLobby)
        {
            granade.enabled = false;
            graple.enabled = false;
            PlayerUI.SetActive(false);
            playerWeapons.SetActive(false);
        }
        else
        {
            granade.enabled = true;
            graple.enabled = true;
            PlayerUI.SetActive(true);
            playerWeapons.SetActive(true);
            FindObjectOfType<ManageWeapon>().ChangeWeapon();

            HealingItem tempRef = PlayerProperty.GetComponentInChildren<HealingItem>();

            if (tempRef != null) tempRef.ResetCount();



        }
    }
    public void tpPlayerToLimbo()
    {
        PlayerMove(new Vector3(1200f,0,2300f));

    }

    public void ResetGenerator()
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

    //public void GotoStageFromShop()
    //{
    //    PlayerMove(PlayerStageSpawn);
    //}

    void PlayerMove(Vector3 newPos)
    {
        playerIns.GetComponentInChildren<PlayerMovement>().transform.position = newPos;
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
