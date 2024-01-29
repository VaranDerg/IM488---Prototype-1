using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartupMenu : BaseMenuController
{
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
}