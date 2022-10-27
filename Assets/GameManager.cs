using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public float FireRateMod = 1,DamageMod=1,MoveSpeedMod=1;

    public int Money;

     PlayerInput playerInput;

    public List<Upgrade> Upgrades = new List<Upgrade>();

   
    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        playerInput.Player.Debug.performed += DebugFunction;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DebugFunction(InputAction.CallbackContext obj)
    {
        AddUpgrade();
    }
    public void AddUpgrade()
    {
        SceneManager.LoadScene("AddUpgrade", LoadSceneMode.Additive);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseAddUpgrade()
    {
        SceneManager.UnloadSceneAsync("AddUpgrade");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
