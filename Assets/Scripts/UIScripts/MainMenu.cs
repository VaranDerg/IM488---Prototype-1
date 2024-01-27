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

    public void StartGame()
    {
        ManagerParent.Instance.Spells.PrepareSpellSelectionState(SpellManager.SpellSelectionMode.BothPlayers);

        int spellSelectScene = ManagerParent.Instance.Game.GetSpellSelectScene();
        SceneTransitions.Instance.LoadSceneWithTransition(SceneTransitions.TransitionType.Fade, spellSelectScene);
    }
}