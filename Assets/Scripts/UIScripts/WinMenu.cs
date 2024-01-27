using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinMenu : BaseMenuController
{
    [SerializeField] private TextMeshProUGUI _playerWinText;

    private void Start()
    {
        int winningPlayer;
        if (ManagerParent.Instance.Game.GetPlayerOneScore() == ManagerParent.Instance.Options.GetPointsToWin())
        {
            winningPlayer = 1;
        }
        else
        {
            winningPlayer = 2;
        }


        PreparePlayerWin(winningPlayer);
    }

    private void PreparePlayerWin(int winningPlayer)
    {
        _playerWinText.text = ManagerParent.Instance.Game.GetPlayerName() + " " + winningPlayer + " is Victorious.";

        ManagerParent.Instance.Game.ResetGame();
    }
}