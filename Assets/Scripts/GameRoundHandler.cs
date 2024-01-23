using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoundHandler : MonoBehaviour
{
    [SerializeField] private float _roundTimerLength;
    private float _currentRoundTime;
    private float _p1Score;
    private float _p2Score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void RoundStart()
    {
        StartCoroutine(RoundTimerCountDown());
    }

    public void P1Won()
    {

    }

    public void P2Won()
    {

    }

    public void RoundEnd()
    {

    }

    private IEnumerator RoundTimerCountDown()
    {
        _currentRoundTime = _roundTimerLength;
        while(true)
        {
            _currentRoundTime -= Time.deltaTime;
        }
    }
}
