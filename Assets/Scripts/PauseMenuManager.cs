using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
    [Header("Mouse")]
    [SerializeField] GameObject Mouse;
    [SerializeField] GameObject MouseSeta;
    [SerializeField] PlayerMovement Player;
    [Header("Credits")]
    [SerializeField] GameObject CreditsObj;
    [SerializeField] GameObject CreditsSeta;


    Volume volume;
    Bloom bloom;
    ChromaticAberration chro;

    Resolution[] resolutions;
    GameObject saveCurrent,setaCurret;
    enum MenuState
    {
        none,Screen,Sound,Mouse,Credits
    };
    MenuState state;
    float bloomvalue, chrovalue;

    AudioSource audio;

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
        audio = GetComponent<AudioSource>();
        Player = FindObjectOfType<PlayerMovement>();
        getResolutions();
        state = MenuState.none;
        volume = FindObjectOfType<Volume>();
        volume.profile.TryGet<Bloom>(out bloom);
        bloomvalue = (float)bloom.intensity;
        volume.profile.TryGet<ChromaticAberration>(out chro);
        chrovalue = (float)chro.intensity;
    }
    private void Update()
    {
        updateValues();
    }
    public void inMenuLoop()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (state == MenuState.Screen || state == MenuState.Sound || state == MenuState.Mouse || state == MenuState.Credits)
            {
                state = MenuState.none;
                saveCurrent.SetActive(false);
                setaCurret.SetActive(false);
                audio.Play();
                
            }

        }
        if (state == MenuState.none)
        {
            SecondSelection.SetActive(false);
        }

    }
    public void PlayMenuSound()
    {
        audio.Play();
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
            audio.Play();
            ScreenSeta.SetActive(true);
            ScreenObj.SetActive(true);
            SecondSelection.SetActive(true);
            state = MenuState.Screen;
            saveCurrent = ScreenObj;
            setaCurret = ScreenSeta;
        }
        else
        {
            audio.Play();
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
        if (state == MenuState.none)
        {
            audio.Play();
            SoundObj.SetActive(true);
            SoundSeta.SetActive(true);
            SecondSelection.SetActive(true);
            saveCurrent = SoundObj;
            setaCurret = SoundSeta;
            state = MenuState.Sound;
        }
        else
        {
            audio.Play();
            saveCurrent.SetActive(false);
            setaCurret.SetActive(false);

            SoundObj.SetActive(true);
            SoundSeta.SetActive(true);
            saveCurrent = SoundObj;
            setaCurret = SoundSeta;
            state = MenuState.Sound;
        }



    }

    public void openMouse()
    {
    
        if (state == MenuState.none)
        {
            audio.Play();
            Mouse.SetActive(true);
            MouseSeta.SetActive(true);
            SecondSelection.SetActive(true);
            saveCurrent = Mouse;
            setaCurret = MouseSeta;
            state = MenuState.Mouse;
        }
        else
        {
            audio.Play();
            saveCurrent.SetActive(false);
            setaCurret.SetActive(false);

            Mouse.SetActive(true);
            MouseSeta.SetActive(true);
            saveCurrent = Mouse;
            setaCurret = MouseSeta;
            state = MenuState.Mouse;
        }
    }

     public void openCredits()
    {
        if(state == MenuState.none)
        {
            audio.Play();
            CreditsObj.SetActive(true);
            CreditsSeta.SetActive(true);
            SecondSelection.SetActive(true);
            saveCurrent = CreditsObj;
            setaCurret = CreditsSeta;
            state = MenuState.Credits;
        }
        else
        {
            audio.Play();
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
            bool check = false;
            foreach (string x in op)
            {
                if (x == option)
                {
                    check = true;
                }
                else check = false;

            }
            if (!check)
            {
                op.Add(option);
            }


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
        audio.Play();
    }
    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        audio.Play();
    }
    public void setVSync(int index)
    {
        QualitySettings.vSyncCount = index;
        audio.Play();
    }
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        audio.Play();
    }
    public void ExistGame()
    {
        audio.Play();
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

    public void setHorizontalSense(float sense)
    {
        Player.ChangeCameraHSense(sense);
    }

    public void setVerticalSense(float sense)
    {
        Player.ChangeCameraVSense(sense);
    }

    public void setBloom(float value)
    {
        bloomvalue = value;
    }

    public void setCromaticAberration(float value)
    {
        chrovalue = value;
    }
    void updateValues()
    {
        VolumeParameter<float> fBloom = new VolumeParameter<float>();
        VolumeParameter<float> fChro = new VolumeParameter<float>();
        fBloom.value = bloomvalue;
        fChro.value = chrovalue;
        bloom.intensity.SetValue(fBloom);
        chro.intensity.SetValue(fChro);

    }

}
