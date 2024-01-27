using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public void DisplayText(int winningPlayer)
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-_rotationDeviation, _rotationDeviation));

        _playerWinsText.text = ManagerParent.Instance.Game.GetPlayerName() + " " + winningPlayer + " Remains.";
        _flavorText.text = _flavorTextPopups[Random.Range(0, _flavorTextPopups.Length)];

        _animator.Play(TEXT_ANIMATION_NAME);
    }
}