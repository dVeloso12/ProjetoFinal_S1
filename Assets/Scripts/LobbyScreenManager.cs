using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
public enum pcState
{
    SelectScreen,chooseWeapon,startScreen,endScreen
};
public class LobbyScreenManager : MonoBehaviour
{
    [SerializeField] ComputerLobby cLoobby;
    [SerializeField] Collider VRColidder;
    [SerializeField] GameObject vrParticle;
    GameManager gameManager;

    [SerializeField] List<GameObject> WeaponNames;

     //GameObject ui;
     //GameObject playerWeapons;
     PlayerMovement PlayerMovement;
    HookShot graple;
    Granade granade;
    //CameraSwitch camcheck;
   public pcState pcState;
    [Header("Ui Components")]
    [Header("Select Screen")]
    [SerializeField] GameObject SelectScreen;
    [SerializeField] GameObject WeaponScreen;
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject endScreen;
    [Header("Choose Weapon Screen")]
    [SerializeField] TextMeshProUGUI weaponName;
    int chooseWeaponIndex=0;
    int maxIndexChooseWeapon;
    [SerializeField] Camera cam;
    Canvas m;
    float timer;

    private void Start()
    {
        //ui = GameObject.Find("PlayerUI");
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        //playerWeapons = GameObject.Find("GunDirection");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //camcheck = GameObject.Find("Player").GetComponent<CameraSwitch>();   
        SaveNames();
        pcState = pcState.startScreen;
        m = GameObject.Find("ScreenUi").GetComponent<Canvas>();
        VRColidder.enabled = false;
        vrParticle.SetActive(false);
        WeaponNames[chooseWeaponIndex].GetComponent<TextMeshProUGUI>().color = Color.gray;



    }

    private void Update()
    {
        UIManager();
        UpdateUI();
    }

    public void UIManager()
    {
        if(cLoobby.canDisable)
        {
            //ui.SetActive(false);
            PlayerMovement.enabled = false;
            //playerWeapons.SetActive(false);
            //gameManager.screenType = ScreenType.outGame;
            gameManager.toComputer();
            timer += Time.deltaTime;
            if(timer >= 2.5f)
            {
                cam.gameObject.SetActive(true);
                timer = 3f;
                if (cLoobby.canChangeScreen)
                {
                    pcState = pcState.SelectScreen;
                    cLoobby.canChangeScreen = false;
                }

            }
           
        }
        if(pcState == pcState.SelectScreen)
        {
            SelectScreen.SetActive(true);
            WeaponScreen.SetActive(false);
            startScreen.SetActive(false);
        }
         if(pcState == pcState.chooseWeapon)
        {
            SelectScreen.SetActive(false);
            WeaponScreen.SetActive(true);
           
        }
         if(pcState == pcState.endScreen)
        {
            vrParticle.SetActive(true);
            SelectScreen.SetActive(false);
            endScreen.SetActive(true);
            cLoobby.canDisable = false;
            //ui.SetActive(true);
            PlayerMovement.enabled = true;
            //playerWeapons.SetActive(true);
            gameManager.outComputer();
            cam.gameObject.SetActive(false);
            VRColidder.enabled = true;
            timer = 0f;
            cLoobby.runReady = true;
            cLoobby.pcMode = false;
            //FindObjectOfType<ManageWeapon>().ChangeWeapon();

            cLoobby.LeavePc();
           
        }
    }

    void SaveNames()
    {
        for(int i = 0; i < WeaponNames.Count; i++)
        {

            WeaponNames[i].GetComponent<TextMeshProUGUI>().text = gameManager.UiSaveTypes[i].ToString();

        }
        //weaponName.text = gameManager.weaponType.ToString();
        maxIndexChooseWeapon = gameManager.UiSaveTypes.Count;
        
    }

    void UpdateUI()
    {
        //Weapon Choose
        //weaponName.text = gameManager.UiSaveTypes[chooseWeaponIndex].ToString();
    }

    public void NextWeapon()
    {
        if(chooseWeaponIndex >= maxIndexChooseWeapon - 1)
        {
            WeaponNames[chooseWeaponIndex].GetComponent<TextMeshProUGUI>().color = Color.white;

            Debug.Log("Cant pass condition");
            chooseWeaponIndex = 0;
            WeaponNames[chooseWeaponIndex].GetComponent<TextMeshProUGUI>().color = Color.gray;

        }
        else
        {
            Debug.Log(chooseWeaponIndex);

            WeaponNames[chooseWeaponIndex].GetComponent<TextMeshProUGUI>().color = Color.white;

            chooseWeaponIndex++;

            if(chooseWeaponIndex <= maxIndexChooseWeapon - 1)
            {
                WeaponNames[chooseWeaponIndex].GetComponent<TextMeshProUGUI>().color = Color.gray;
            }

            


        }

        

        
    }
    public void toChooseWeaponScreen()
    {
        pcState = pcState.chooseWeapon;
    }
    public void toSelectScreen()
    {
        pcState = pcState.SelectScreen;
    }
    public void PrepareRun()
    {
        pcState = pcState.endScreen;
        gameManager.weaponType = gameManager.UiSaveTypes[chooseWeaponIndex];
       
        
    }

}
