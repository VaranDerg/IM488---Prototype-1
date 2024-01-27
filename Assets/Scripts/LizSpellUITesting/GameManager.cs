using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int WIN_SCENE = 5;
    private const int SPELL_SCENE = 1;
    private const string PLAYER_NAME = "Plasmo";

    private int _playerOneScore;
    private int _playerTwoScore;

    [SerializeField] private float _winDelay;

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

        StartCoroutine(ToNextGamePhaseProcess());
    }

    private IEnumerator ToNextGamePhaseProcess()
    {
        int pointsToWin = ManagerParent.Instance.Options.GetPointsToWin();
        int nextScene;
        if (_playerOneScore == pointsToWin || _playerTwoScore == pointsToWin)
        {
            ManagerParent.Instance.Game.ResetGame();

            nextScene = WIN_SCENE;
        }
        else
        {
            nextScene = SPELL_SCENE;
        }

        yield return new WaitForSeconds(_winDelay);

        SceneTransitions.Instance.LoadSceneWithTransition(SceneTransitions.TransitionType.LeftRight, nextScene);
    }

    public void ResetGame()
    {
        _playerOneScore = 0;
        _playerTwoScore = 0;

        ManagerParent.Instance.Spells.ClearSpellsForBothPlayers();
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
}