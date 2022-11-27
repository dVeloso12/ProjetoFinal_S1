using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum WeaponType
{
    Shotgun,
    AR,
    Pistol
};

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public WeaponType weaponType;
    public List<WeaponType> UiSaveTypes;
    //public ScreenType screenType;

    public float FireRateMod = 1,DamageMod=1,MoveSpeedMod=1,HSMod=2,CDMod=1;

    public int Money;

     PlayerInput playerInput;

    public List<Upgrade> Upgrades = new List<Upgrade>();

    [HideInInspector]
    public List<int>[] Upgrade_Rarity=new List<int>[3];

    public List<GameObject> EnemyList=new List<GameObject>();
    //[HideInInspector]
    //public List<Upgrade> GeneralUpgrades, WeaponUpgrades;

    public int gameState;

    [HideInInspector]
    public SurvivalScript surv;

    public bool SurvStage = false;

    bool statsOpen=false;
    public GameplayOrganize orggame;
    bool unloadDeadScene;
    float timer;


    private void Awake()
    {

        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        
        playerInput.Player.Debug.performed += DebugFunction;

        playerInput.Player.OpenStats.performed += OpenStats;

        
    }
    private void Update()
    {
        if(unloadDeadScene && timer < 4f)
        {
            timer += Time.deltaTime;
        }
        else if(unloadDeadScene && timer >4f)
        {
            SceneManager.UnloadSceneAsync("GameOver");
            unloadDeadScene = false;
        }
    }

    void Start()
    {
       
        CreateUpgradeLists();
        SaveUiNames();
    }
   
    public void ResetStage()
    {
        foreach (GameObject enemy in EnemyList)
        {
            enemy.SetActive(false);
        }

        EnemyList.Clear();
        SurvStage = false;
    }

    void SaveUiNames()
    {
        UiSaveTypes.Add(WeaponType.Shotgun);
        UiSaveTypes.Add(WeaponType.AR); 
        UiSaveTypes.Add(WeaponType.Pistol);
    }

    public void DebugFunction(InputAction.CallbackContext obj)
    {
        gameState = 1;
        AddUpgrade();
    }
    public void AddUpgrade()
    {
        if(gameState==0)
        SceneManager.LoadScene("AddUpgrade", LoadSceneMode.Additive);
        else if(gameState==1)
            SceneManager.LoadScene("ShopUpgrade", LoadSceneMode.Additive);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void OpenStats(InputAction.CallbackContext obj)
    {
        if (!statsOpen)
        {
            SceneManager.LoadScene("PlayerStats", LoadSceneMode.Additive);
            statsOpen = true;
        }
        else
        {
            SceneManager.UnloadSceneAsync("PlayerStats");
            statsOpen = false;
        }

    }
    
    public void ChangeWeapon()
    {
        //ManageWeapon mg = FindObjectOfType
        //mg.ChangeWeapon();
    }
    public void DeadEnemy(GameObject Enemy)
    {
        if (SurvStage)
            surv.nObjective--;
        EnemyList.Remove(Enemy);

    }

    public void CloseAddUpgrade()
    {
        if (gameState == 0)
            SceneManager.UnloadSceneAsync("AddUpgrade");
        else if (gameState == 1)
            SceneManager.UnloadSceneAsync("ShopUpgrade");
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        orggame.toLobby = true;
        unloadDeadScene = true;
        //Time.timeScale = 0f;
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
    }
    public void toComputer()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void outComputer()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CreateUpgradeLists()
    {
        for (int j = 0; j < 3; j++)
            Upgrade_Rarity[j] = new List<int>();

        int i = 0;
        foreach(Upgrade upgrade in Upgrades)
        {
            switch (upgrade.Rarity)
            {
                case 1:
                    Upgrade_Rarity[0].Add(i);
                    break;
                case 2:
                    Upgrade_Rarity[1].Add(i);
                    break;
                case 3:
                    Upgrade_Rarity[2].Add(i);
                    break;

            }
            i++;  
        }
    }
}
