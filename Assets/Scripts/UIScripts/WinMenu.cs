using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinMenu : BaseMenuController
{
    [SerializeField] private TextMeshProUGUI _playerWinText;

    private void Start()
    {
        Debug.Log("Winning player is being determined randomly until implementation is complete.");

        PreparePlayerWin(Random.Range(1, 3));
    }

    private void PreparePlayerWin(int winningPlayer)
    {
        _playerWinText.text = $"Player {winningPlayer} Wins!";
    }
}