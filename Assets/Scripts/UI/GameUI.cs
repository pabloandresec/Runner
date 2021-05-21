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
        SetFadeDirection(0);
        FadeSwapMenu(mainMenus[0], mainMenus[1], () =>
          {
              gamePaused = true;
              Time.timeScale = 0;
          });
    }
    public void UnPauseGame()
    {
        SetFadeDirection(2);
        Time.timeScale = 1;
        FadeSwapMenu(mainMenus[1], mainMenus[0], () =>
        {
            gamePaused = false;
        });
    }
}
