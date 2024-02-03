using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

/// <summary>
/// Author: Liz
/// Description: Runs a timer for the game. Picks a random winner at the end.
/// </summary>
public class TempGameTimer : MonoBehaviour
{
    [SerializeField] private float _timerLength;
    [Space]
    [SerializeField] private TextMeshProUGUI _timerText;

    private float _currentTime;
    [HideInInspector] public bool TimerEnded;

    private void Start()
    {
        _currentTime = _timerLength;
    }

    private void Update()
    {
        TickTimer(Time.deltaTime);
    }

    private void SetTimerText()
    {
        var ts = TimeSpan.FromSeconds(_currentTime);
        _timerText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }

    private void TickTimer(float amount)
    {
        if (TimerEnded)
        {
            return;
        }

        SetTimerText();
        _currentTime -= amount;

        if (_currentTime <= 0)
        {
            _currentTime = 0;
            TimerEnded = true;

            //PickRandomWinner();
            ManagerParent.Instance.Game.RoundTie();
        }
    }


    private void PickRandomWinner()
    {
        int playerWin = UnityEngine.Random.Range(1, 3);

        FindObjectOfType<WinningPlayerText>().DisplayTimeoutText(playerWin);

        ManagerParent.Instance.Game.IncreasePlayerScore(playerWin);
    }
}