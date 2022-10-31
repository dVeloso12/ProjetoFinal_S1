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

    public float FireRateMod = 1,DamageMod=1,MoveSpeedMod=1;

    public int Money;

     PlayerInput playerInput;

    public List<Upgrade> Upgrades = new List<Upgrade>();

    TextMeshProUGUI MoneyText;

    [HideInInspector]
    public List<Upgrade> GeneralUpgrades, WeaponUpgrades;

    public int gameState;

    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        playerInput.Player.Debug.performed += DebugFunction;

        //MoneyText = GameObject.Find("MoneyC").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        CreateUpgradeLists();
    }

    // Update is called once per frame
    void Update()
    {
        //MoneyText.text = Money.ToString()+" $";
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

    void CreateUpgradeLists()
    {
        foreach(Upgrade upgrade in Upgrades)
        {
            if (upgrade.General)
                GeneralUpgrades.Add(upgrade);
            else if (upgrade.WeaponAffected == weaponType)
                WeaponUpgrades.Add(upgrade);
        }
    }
}
