using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Author: Liz
/// Description: Win menu functionality
/// </summary>
public class WinMenu : BaseMenuController
{
    [SerializeField] private TextMeshProUGUI _playerWinText;

    /// <summary>
    /// Gets the integer of the winning player.
    /// </summary>
    private void Start()
    {
        int winningPlayer;
        if (ManagerParent.Instance.Game.GetPlayerOneScore() == ManagerParent.Instance.Options.GetPointsToWin())
        {
            winningPlayer = 1;
            _playerWinText.color = MultiplayerManager.Instance.GetColorFromPlayer(Player.one);
        }
        else
        {
            winningPlayer = 2;
            _playerWinText.color = MultiplayerManager.Instance.GetColorFromPlayer(Player.two);
        }

        PreparePlayerWin(winningPlayer);
    }

    /// <summary>
    /// Prepares the text.
    /// </summary>
    /// <param name="winningPlayer">The player who won</param>
    private void PreparePlayerWin(int winningPlayer)
    {
        _playerWinText.text = ManagerParent.Instance.Game.GetPlayerName() + " " + winningPlayer + " is Victorious.";

        ManagerParent.Instance.Game.ResetGame();
    }
}