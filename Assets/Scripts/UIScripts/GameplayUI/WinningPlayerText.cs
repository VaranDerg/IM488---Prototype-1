using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Author: Liz
/// Description: A cool visual for the player that one.
/// </summary>
public class WinningPlayerText : BaseUIElement
{
    private const string TEXT_ANIMATION_NAME = "PlayerWinTextEnter";

    [SerializeField] [Range(0, 90)] private float _rotationDeviation;
    [Space]
    [SerializeField] private string[] _flavorTextPopups;
    [Space]
    [SerializeField] private TextMeshProUGUI _playerWinsText;
    [SerializeField] private TextMeshProUGUI _flavorText;
    [SerializeField] private Animator _animator;

    /// <summary>
    /// Displays the text with a random win message.
    /// </summary>
    /// <param name="winningPlayer">The player that won the round</param>
    public void DisplayText(int winningPlayer)
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-_rotationDeviation, _rotationDeviation));

        Color c;
        if (winningPlayer == 1)
        {
            c = MultiplayerManager.Instance.GetColorFromPlayer(Player.one);
        }
        else
        {
            c = MultiplayerManager.Instance.GetColorFromPlayer(Player.two);
        }

        _playerWinsText.color = c;
        _flavorText.color = c;

        _playerWinsText.text = ManagerParent.Instance.Game.GetPlayerName() + " " + winningPlayer + " Remains.";
        _flavorText.text = _flavorTextPopups[Random.Range(0, _flavorTextPopups.Length)];

        _animator.Play(TEXT_ANIMATION_NAME);
    }

    public void DisplayTimeoutText(int chosenPlayer)
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        _playerWinsText.text = ManagerParent.Instance.Game.GetPlayerName() + " " + chosenPlayer + " Chosen as Winner.";
        _flavorText.text = "Winner chosen due to disadvantage.";

        _animator.Play(TEXT_ANIMATION_NAME);
    }

    public void DisplayTieTimeoutText()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        _playerWinsText.text = "Tie!";
        _flavorText.text = "Starting Next Round";

        _animator.Play(TEXT_ANIMATION_NAME);
    }
}