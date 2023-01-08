using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    bool inMenu;
    private void Awake()
    {
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
    }
    private void Start()
    {
        if(PauseMenuManager.instance != null)
        {
            PauseMenuManager.instance.canReset();
            SceneManager.UnloadSceneAsync("PauseMenu");
        }

    }
    private void Update()
    {
        MenuPauseManager();
    }

    public void MenuPauseManager()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!inMenu)
            {
                LoadPauseMenu();
                inMenu = true;
            }
            else
            {
                if (!PauseMenuManager.instance.checkWhenLeave(inMenu))
                {
                    PauseMenuManager.instance.PlayMenuSound();
                    UnloadPauseMenu();
                    inMenu = false;
                }
            }

        }
        if (inMenu)
        {
            PauseMenuManager.instance.inMenuLoop();
        }
    }
    public void LoadPauseMenu()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        try
        {
            FindObjectOfType<GunController>().PauseManager();
        }
        catch { };
        PauseMenuManager.instance.setMenu();
    }
    public void UnloadPauseMenu()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuManager.instance.canReset();
    }

}
