using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoundHandler : MonoBehaviour
{
    [SerializeField] private float _roundTimerLength;
    [SerializeField] private Vector3 p1StartLoc;
    [SerializeField] private Vector3 p2StartLoc;
    private float _currentRoundTime;
    private float _p1Score;
    private float _p2Score;

    internal static GameRoundHandler instance;
    internal GameObject P1;
    internal GameObject P2;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void AssignPlayer(GameObject inGO)
    {
        if (P1 == null)
        {
            P1 = inGO;
            P1.GetComponent<PlayerManager>().PlayerStartingLocation(p1StartLoc);
            return;
        }
        P2 = inGO;
        P2.GetComponent<PlayerManager>().PlayerStartingLocation(p2StartLoc);
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
