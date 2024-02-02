using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartupMenu : BaseMenuController
{
    bool hasP1Joined = false;

    /// <summary>
    /// Prompts player 2 to press a button.
    /// </summary>
    public void OpenNextPage()
    {
        SetPageActive(MenuPages[1]);
    }

    /// <summary>
    /// Opens the main menu.
    /// </summary>
    public void SetupComplete()
    {
        LoadScene(1);
    }

    public void OnPlayerJoined()
    {
        //Debug.Log("Player Joined");

        if (!hasP1Joined)
        {
            OpenNextPage();
            hasP1Joined = true;
        }
        else
        {
            //Debug.Log("Setup Done!");
            SetupComplete();
        }
    }
}