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
    private const int WIN_SCENE = 5;
    private const int SPELL_SCENE = 1;
    private const string PLAYER_NAME = "Plasmo";
    private const float _winDelayBeforeSceneLoad = 1.5f;

    //Scenes for inspector assignment.
    [SerializeField] private int _spellSelectScene;
    [SerializeField] private int[] _arenaScenes;

    //Holds player scores.
    private int _playerOneScore;
    private int _playerTwoScore;

    /// <summary>
    /// Increases the score for a player.
    /// </summary>
    /// <param name="player">Which player to increase the score of.</param>
    /// <param name="winDelay">How long a win message is displayed.</param>
    public void IncreasePlayerScore(int player)
    {
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

        StartCoroutine(ToNextGamePhaseProcess(_winDelayBeforeSceneLoad));
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
            nextScene = WIN_SCENE;
        }
        else
        {
            nextScene = SPELL_SCENE;
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
        return PLAYER_NAME;
    }

    public int GetSpellSelectScene()
    {
        return _spellSelectScene;
    }
}