using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager instance;
    [Header("MainScreens")]
    [SerializeField] GameObject MainSelection;
    [SerializeField] GameObject SecondSelection;
    [SerializeField] GameObject gameNameText;
    [Header("MainScreen")]
    [Header("Screen")]
    [SerializeField] GameObject ScreenObj;
    [SerializeField] GameObject ScreenSeta;
    [SerializeField] TMP_Dropdown resolution, graphics,vsync;
    [SerializeField] Toggle fullcreen;
    [Header("Sound")]
    [SerializeField] GameObject SoundObj;
    [SerializeField] GameObject SoundSeta;
    [SerializeField] AudioMixer Effects, Music;
    [Header("Credits")]
    [SerializeField] GameObject CreditsObj;
    [SerializeField] GameObject CreditsSeta;


    Resolution[] resolutions;
    GameObject saveCurrent,setaCurret;
    enum MenuState
    {
        none,Screen,Sound,Credits
    };
     MenuState state;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
       
            instance = this;
            DontDestroyOnLoad(gameObject);
        
       
    }
    private void Start()
    {
        getResolutions();
        state = MenuState.none;
      
    }
    public void inMenuLoop()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (state == MenuState.Screen || state == MenuState.Sound || state == MenuState.Credits)
            {
                state = MenuState.none;
                saveCurrent.SetActive(false);
                setaCurret.SetActive(false);
            }
           
        }
        if (state == MenuState.none)
        {
            SecondSelection.SetActive(false);
        }

    }
    public bool checkWhenLeave(bool start)
    {
        if(start)
        {
            if (state == MenuState.none)
                return false;

            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void setMenu()
    {
        MainSelection.SetActive(true);
        gameNameText.SetActive(true);
        state = MenuState.none;
    }
   public void canReset()
    {
        MainSelection.SetActive(false);
        gameNameText.SetActive(false);
        SecondSelection.SetActive(false);
        if (saveCurrent != null)
            saveCurrent.SetActive(false);
        if (setaCurret != null)
            setaCurret.SetActive(false);
        state = MenuState.none;
    }
    public void openScreen()
    {
        if(state == MenuState.none)
        {
            ScreenSeta.SetActive(true);
            ScreenObj.SetActive(true);
            SecondSelection.SetActive(true);
            state = MenuState.Screen;
            saveCurrent = ScreenObj;
            setaCurret = ScreenSeta;
        }
        else
        {
            saveCurrent.SetActive(false);
            setaCurret.SetActive(false);

            ScreenSeta.SetActive(true);
            ScreenObj.SetActive(true);
            state = MenuState.Screen;
            saveCurrent = ScreenObj;
            setaCurret = ScreenSeta;

        }      
    }

    public void openSound()
    {
        if(state == MenuState.none)
        {
            SoundObj.SetActive(true);
            SoundSeta.SetActive(true);
            SecondSelection.SetActive(true);
            saveCurrent = SoundObj;
            setaCurret = SoundSeta;
            state = MenuState.Sound;
        }
        else
        {
            saveCurrent.SetActive(false);
            setaCurret.SetActive(false);

            SoundObj.SetActive(true);
            SoundSeta.SetActive(true);
            saveCurrent = SoundObj;
            setaCurret = SoundSeta;
            state = MenuState.Sound;
        }
    }

    public void openCredits()
    {
        if(state == MenuState.none)
        {
            CreditsObj.SetActive(true);
            CreditsSeta.SetActive(true);
            SecondSelection.SetActive(true);
            saveCurrent = CreditsObj;
            setaCurret = CreditsSeta;
            state = MenuState.Credits;
        }
        else
        {
            saveCurrent.SetActive(false);
            setaCurret.SetActive(false);

            CreditsObj.SetActive(true);
            CreditsSeta.SetActive(true);
            saveCurrent = CreditsObj;
            setaCurret = CreditsSeta;
            state = MenuState.Credits;

        }
    }
   
    void getResolutions()
    {
        resolutions = Screen.resolutions;
       

        resolution.ClearOptions();

        int currentRes = 0;
        List<string> op = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            op.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }
        resolution.AddOptions(op);
        resolution.value = currentRes;
        resolution.RefreshShownValue();
    }
    public void SetFullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }
    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    public void setVSync(int index)
    {
        QualitySettings.vSyncCount = index;
    }
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    public void ExistGame()
    {
        Application.Quit();
    }
    public void setVolumeEffects(float vol)
    {
        Effects.SetFloat("EffectsVolume",vol); //Ajustar mais tarde
    }
    public void setVolumeMusic(float vol)
    {
        Music.SetFloat("MusicVolume", vol); //Ajustar mais tarde 
    }


}
