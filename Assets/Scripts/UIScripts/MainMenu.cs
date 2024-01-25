using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains Main Menu-specific functionality.
/// </summary>
public class MainMenu : BaseMenuController
{
    /// <summary>
    /// Closes the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
