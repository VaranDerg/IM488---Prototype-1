using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Liz
/// Description: Manages basic game processes.
/// </summary>
public class GameManager : MonoBehaviour
{
    ///Constants for gameplay.

    //Scenes for inspector assignment.
    [SerializeField] private string _playerName = "Plasmo";
    [Space]
    [SerializeField] private int _spellSelectScene = 2;
    [SerializeField] private int[] _arenaScenes;
    [SerializeField] private int _winScene = 8;
    [Space]
    [SerializeField] private float _winDelay = 1.5f;

    //Holds player scores.
    private int _playerOneScore;
    private int _playerTwoScore;

    //Marked true after a player's score increases and marked false after a spell is selected. Prevents odd behavior.
    public bool PlayerHasWonRound { get; set; }

    /// <summary>
    /// Increases the score for a player.
    /// </summary>
    /// <param name="player">Which player to increase the score of.</param>
    /// <param name="winDelay">How long a win message is displayed.</param>
    public void IncreasePlayerScore(int player)
    {
        PlayerHasWonRound = true;

        if (player == 1)
        {
            _playerOneScore++;
            ManagerParent.Instance.Spells.PrepareSpellSelectionState(SpellManager.SpellSelectionMode.PlayerTwo);
        }
        else
        {
            _playerTwoScore++;
            ManagerParent.Instance.Spells.PrepareSpellSelectionState(SpellManager.SpellSelectionMode.PlayerOne);
        }

        FindObjectOfType<GameplayMenu>().DisplayWin(player);

        StartCoroutine(ToNextGamePhaseProcess(_winDelay));
    }

    /// <summary>
    /// Moves to the next game phase, either the spell select scene or win scene.
    /// </summary>
    /// <param name="waitTime">The amount of wait time.</param>
    private IEnumerator ToNextGamePhaseProcess(float waitTime)
    {
        int pointsToWin = ManagerParent.Instance.Options.GetPointsToWin();
        int nextScene;
        if (_playerOneScore == pointsToWin || _playerTwoScore == pointsToWin)
        {
            nextScene = _winScene;
        }
        else
        {
            nextScene = _spellSelectScene;
        }

        yield return new WaitForSeconds(waitTime);

        SceneTransitions.Instance.LoadSceneWithTransition(SceneTransitions.TransitionType.LeftRight, nextScene);
    }

    /// <summary>
    /// Resets scores and spells.
    /// </summary>
    public void ResetGame()
    {
        _playerOneScore = 0;
        _playerTwoScore = 0;

        ManagerParent.Instance.Spells.ClearSpellsForBothPlayers();
    }

    /// <summary>
    /// Used for loading a random Arena.
    /// </summary>
    /// <returns>The index of a random arena scene.</returns>
    public int GetRandomArenaScene()
    {
        return _arenaScenes[Random.Range(0, _arenaScenes.Length)];
    }

    /// <summary>
    /// Used for loading a random Arena.
    /// </summary>
    /// <param name="excludeScene">A scene you want to exclude from selection. Pass in current build index to prevent repeated arenas.</param>
    /// <returns>The index of a random arena scene. The excludeScene is not possible.</returns>
    public int GetRandomArenaScene(int excludeScene)
    {
        List<int> possibleArenaScenes = new List<int>();
        foreach (int scene in _arenaScenes)
        {
            if (scene != excludeScene)
            {
                possibleArenaScenes.Add(scene);
            }
        }

        return possibleArenaScenes[Random.Range(0, _arenaScenes.Length)];
    }

    //Basic getters

    public int GetPlayerOneScore()
    {
        return _playerOneScore;
    }

    public int GetPlayerTwoScore()
    {
        return _playerTwoScore;
    }

    public string GetPlayerName()
    {
        return _playerName;
    }

    public int GetSpellSelectScene()
    {
        return _spellSelectScene;
    }
}