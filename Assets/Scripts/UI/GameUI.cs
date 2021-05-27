using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class GameUI : MenuController
{
    private bool gamePaused = false;

    private void Start()
    {
        SwitchMenu(0);
    }

    private void Update()
    { 
        if(InputSystem.GetDevice<Keyboard>().pKey.wasPressedThisFrame && !swapingMenus)
        {
            if(gamePaused)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
            
        }
    }

    public void PauseGame()
    {
        SwitchMenu(1);
        gamePaused = true;
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
        SwitchMenu(0);
    }

    public void ShowHelp()
    {
        SwitchMenu(2);
        gamePaused = true;
        Time.timeScale = 0;
    }
}
