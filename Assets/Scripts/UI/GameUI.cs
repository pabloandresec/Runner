using UnityEngine;
using System.Collections;

public class GameUI : MenuController
{

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(mainMenus[0].activeInHierarchy)
            {
                SetFadeDirection(2);
                FadeOutMenu(mainMenus[0]);
            }
            else
            {
                FadeInMenu(mainMenus[0]);
                SetFadeDirection(0);
            }
        }
    }
}
