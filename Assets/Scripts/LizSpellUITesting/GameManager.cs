using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int WIN_SCENE = 5;
    private const int SPELL_SCENE = 1;
    private const string PLAYER_NAME = "Plasmo";

    [SerializeField] private int _spellSelectScene;
    [SerializeField] private int[] _arenaScenes;

    private int _playerOneScore;
    private int _playerTwoScore;

    public void IncreasePlayerScore(int player, float winDelay)
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

        StartCoroutine(ToNextGamePhaseProcess(winDelay));
    }

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