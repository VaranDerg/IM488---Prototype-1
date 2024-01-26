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

    private void Start()
    {
        _scoreText.text = GameOptionsAndProgress.PlayerOneScore + " - " + GameOptionsAndProgress.PlayerTwoScore;
    }
}