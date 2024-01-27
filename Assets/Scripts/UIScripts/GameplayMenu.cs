using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Author: Liz
/// Description: Provides gameplay-menu specific behavior. 
/// </summary>
public class GameplayMenu : BaseUIElement
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _firstToText;
    [SerializeField] private WinningPlayerText _winText;

    private void Start()
    {
        _firstToText.text = "First to " + ManagerParent.Instance.Options.GetPointsToWin();

        SetScoreText();
    }

    public void SetScoreText()
    {
        _scoreText.text = ManagerParent.Instance.Game.GetPlayerOneScore() + " - " + ManagerParent.Instance.Game.GetPlayerTwoScore();
    }

    public void DisplayWin(int player)
    {
        SetScoreText();
        _winText.DisplayText(player);
    }

    public void TestPlayerWin(int player)
    {
        ManagerParent.Instance.Game.IncreasePlayerScore(player);
    }
}