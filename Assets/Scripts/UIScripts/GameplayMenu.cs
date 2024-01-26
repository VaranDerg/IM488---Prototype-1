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
    [SerializeField] private WinningPlayerText _winText;

    private void Start()
    {
        SetScoreText();
    }

    public void SetScoreText()
    {
        _scoreText.text = GameOptionsAndProgress.PlayerOneScore + " - " + GameOptionsAndProgress.PlayerTwoScore;
    }

    public void DisplayWin(int player)
    {
        SetScoreText();
        _winText.DisplayText(player);
    }
}