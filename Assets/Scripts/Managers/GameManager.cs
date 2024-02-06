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
    [SerializeField] private int _mainMenuScene = 1;
    [SerializeField] private int _spellSelectScene = 2;
    [SerializeField] private int _startingArena;
    [SerializeField] private int[] _arenaScenes;
    [SerializeField] private int _winScene = 9;
    [Space]
    [SerializeField] private float _winDelay = 1.5f;
    [SerializeField] private float _tieWindow;
    [SerializeField] private bool _tieFavorsLosingPlayer;

    //Holds player scores.
    private int _playerOneScore;
    private int _playerTwoScore;

    //Marked true after a player's score increases and marked false after a spell is selected. Prevents odd behavior.
    public bool PlayerHasWonRound { get; set; }
    private Coroutine _winDelayCoroutine;
    private int _deadPlayersCount = 0;
    private PlayerHealth _lastDeadPlayer;


    public void StartTieWindow(PlayerHealth player)
    {
        _deadPlayersCount++;
        _lastDeadPlayer = player;
        if (_winDelayCoroutine == null)
            StartCoroutine(TieWindow());
    }

    public IEnumerator TieWindow()
    {
        yield return new WaitForSeconds(_tieWindow);
        
        if(_deadPlayersCount > 1)
        {
            HandleRoundTie();
        }
        else
        {
            IncreasePlayerScore(_lastDeadPlayer.NotThisPlayer());
        }

        foreach(PlayerHealth ph in FindObjectsOfType<PlayerHealth>())
        {
            ph.StopPlayerAndSpellsOnDeath();
        }

        _deadPlayersCount = 0;
        _lastDeadPlayer = null;
    }
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

        if (FindObjectOfType<TempGameTimer>())
        {
            if (!FindObjectOfType<TempGameTimer>().TimerEnded)
            {
                FindObjectOfType<GameplayMenu>().DisplayWin(player);
            }
        }

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
    /// Favor losing player in tie
    /// Otherwise go to next round
    /// </summary>
    public void HandleRoundTie()
    {
        if(_tieFavorsLosingPlayer)
        {
            int losingPlayer = GetLosingPlayer();
            if (losingPlayer != 0)
            {
                FindObjectOfType<WinningPlayerText>().DisplayTimeoutText(losingPlayer);
                IncreasePlayerScore(losingPlayer);
                return;
            }
        }
        StartCoroutine(RoundTie(_winDelay));
    }

    private IEnumerator RoundTie(float waitTime)
    {
        FindObjectOfType<WinningPlayerText>().DisplayTieTimeoutText();
        yield return new WaitForSeconds(waitTime);
        int arenaScene = ManagerParent.Instance.Game.GetNextArenaScene();
        SceneTransitions.Instance.LoadSceneWithTransition(SceneTransitions.TransitionType.LeftRight, arenaScene);
        
    }


    /// <summary>
    /// Goes to starting arena or first round
    /// Goes to a random arena every other round
    /// </summary>
    /// <returns></returns>
    public int GetNextArenaScene()
    {
        if(_playerOneScore + _playerTwoScore == 0)
        {
            return _startingArena;
        }
        return GetRandomArenaScene();
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

    public int GetLosingPlayer()
    {
        if (_playerOneScore > _playerTwoScore) return 2;
        else if (_playerOneScore < _playerTwoScore) return 1;
        //No player losing
        return 0;
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

    public int GetMainMenuScene()
    {
        return _mainMenuScene;
    }
}