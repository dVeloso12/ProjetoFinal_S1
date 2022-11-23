using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("MainScreens")]
    [SerializeField] GameObject MainSelection;
    [SerializeField] GameObject SecondSelection;
    [Header("MainScreen")]
    [Header("Screen")]
    [SerializeField] GameObject ScreenObj;
    [SerializeField] GameObject ScreenSeta;
    [SerializeField] TMP_Dropdown resolution, graphics,vsync;
    [SerializeField] Toggle fullcreen;
    [Header("Sound")]
    [SerializeField] GameObject SoundObj;
    [SerializeField] GameObject SoundSeta;
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

    private void Start()
    {
        getResolutions();
        state = MenuState.none;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(state == MenuState.Screen || state == MenuState.Sound || state == MenuState.Credits)
            {
                state = MenuState.none;
                saveCurrent.SetActive(false);
                setaCurret.SetActive(false);
            }
            else if(state == MenuState.none)
            {
                int y = SceneManager.GetActiveScene().buildIndex;
                SceneManager.UnloadSceneAsync(y);
            }
            
        }
        if(state ==  MenuState.none)
        {
            SecondSelection.SetActive(false);
        }
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
        Debug.Log(index);
    }
    public void SetQuality(int index)
    {
        Debug.Log(index);

        QualitySettings.SetQualityLevel(index);
    }

}
