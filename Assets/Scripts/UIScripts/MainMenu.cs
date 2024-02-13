using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Liz
/// Description: 
/// </summary>
public class MainMenu : BaseMenuController
{
    private static bool _seenFirstTimeText;
    [SerializeField] private GameObject _firstTimeText;

    private void Start()
    {
        if (!_seenFirstTimeText)
        {
            _seenFirstTimeText = true;
            _firstTimeText.SetActive(true);
        }
        else
        {
            _firstTimeText.SetActive(false);
        }
    }

    /// <summary>
    /// Closes the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisableHandbookText()
    {
        _firstTimeText.SetActive(false);
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