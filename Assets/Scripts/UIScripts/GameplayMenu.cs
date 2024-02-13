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
    //How long the win message is displayed
    [SerializeField] private float _winTextShowTime;
    [Space]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _firstToText;
    [SerializeField] private WinningPlayerText _winText;

    /// <summary>
    /// Sets up the X to win text
    /// </summary>
    private void Start()
    {
        _firstToText.text = "First to " + ManagerParent.Instance.Options.GetPointsToWin();

        SetScoreText();
    }

    /// <summary>
    /// Updates the score text
    /// </summary>
    public void SetScoreText()
    {
        _scoreText.text = ManagerParent.Instance.Game.GetPlayerOneScore() + " - " + ManagerParent.Instance.Game.GetPlayerTwoScore();
    }

    /// <summary>
    /// Displays a win message
    /// </summary>
    /// <param name="player">The player that won</param>
    public void DisplayWin(int player)
    {
        SetScoreText();
        _winText.DisplayText(player);
    }

    /// <summary>
    /// Debug function for testing a win. Normally call this from the GameManager
    /// </summary>
    /// <param name="player">The player who won</param>
    public void TestPlayerWin(int player)
    {
        ManagerParent.Instance.Game.IncreasePlayerScore(player);
    }

    public float GetWinDelay()
    {
        return _winTextShowTime;
    }
}