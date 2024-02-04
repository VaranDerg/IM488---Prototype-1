using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Liz
/// Description: 
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

    /// <summary>
    /// Starts the game with the correct spell selection state and transition
    /// </summary>
    public void StartGame()
    {

        ManagerParent.Instance.Spells.AssignStarterSpellToPlayers();

        ManagerParent.Instance.Spells.PrepareSpellSelectionState(SpellManager.SpellSelectionMode.BothPlayers);

        int spellSelectScene = ManagerParent.Instance.Game.GetSpellSelectScene();
        SceneTransitions.Instance.LoadSceneWithTransition(SceneTransitions.TransitionType.Fade, spellSelectScene);
    }
}