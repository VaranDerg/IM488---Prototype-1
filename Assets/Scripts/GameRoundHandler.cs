using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoundHandler : MonoBehaviour
{
    [SerializeField] private float _roundTimerLength;
/*    [SerializeField] private Vector3 p1StartLoc;
    [SerializeField] private Vector3 p2StartLoc;*/
    //[SerializeField] private PlayerHealth _pHealth;
    private const int _winsRequired = 3;
    private float _currentRoundTime;
    int _p1Wins, _p2Wins;
    private Coroutine _timerCountdown;

    internal GameObject P1;
    internal GameObject P2;

    public static GameRoundHandler Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        RoundStart();
    }

    /*void Update()
    {
        DetermineWhoDied();
    }*/



/*    public Player AssignPlayer(GameObject inGO)
    {
        if (P1 == null)
        {
            P1 = inGO;
            P1.GetComponent<PlayerManager>().PlayerStartingLocation(p1StartLoc);
            return Player.one;
        }
        P2 = inGO;
        P2.GetComponent<PlayerManager>().PlayerStartingLocation(p2StartLoc);
        return Player.two;
    }*/

    void RoundStart() => _timerCountdown = StartCoroutine(RoundTimerCountDown());

    void Awake()
    {
        Reset();
        EstablishSingleton();
    }

    void EstablishSingleton()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);

    }
/*    void OnEnable() => Instance = this;
    void OnDisable() => Instance = null;
    void OnDestroy() => OnDisable();*/

    public void Reset()
    {
        _p1Wins = _p2Wins = 0;
    }

    public void DetermineWhoDied(Player whichPlayer)
    {
        switch(whichPlayer)
        {
            case (Player.one):
                P2Won();
                return;
            case (Player.two):
                P1Won();
                return;
            default:
                return;
        }
        /*if (_pHealth.HasDied(Player.one) == true)
        {
            P2Won();
        }
        else if (_pHealth.HasDied(Player.two) == true)
        {
            P1Won();
        }
        else
            return;*/
    }

    public void P1Won()
    {
        RecordRoundWinner(winner: Player.one);
        ManagerParent.Instance.Game.IncreasePlayerScore(1);
        RoundEnd();
        //P1.GetComponent<PlayerManager>().PlayerStartingLocation(p1StartLoc);
    }

    public void P2Won()
    {
        RecordRoundWinner(winner: Player.two);
        ManagerParent.Instance.Game.IncreasePlayerScore(2);
        RoundEnd();
        //P2.GetComponent<PlayerManager>().PlayerStartingLocation(p2StartLoc);
    }

    private IEnumerator RoundTimerCountDown()
    {
        _currentRoundTime = _roundTimerLength;
        while(true)
        {
            _currentRoundTime -= Time.deltaTime;
            yield return null;
        }
    }

    public void RecordRoundWinner(Player winner) => setWins(winner, getWins(winner) + 1);

    public int CurrentWinsOf(Player player) => getWins(player);

    public bool FinalRound() => _p1Wins == _winsRequired || _p2Wins == _winsRequired;

    private int getWins(Player player) => player == Player.two ? _p2Wins : _p1Wins;

    private void setWins(Player player, int value)
    {
        if (player == Player.two) _p2Wins = value;
        else _p1Wins = value;
    }

    public void RoundEnd()
    {
        if(_timerCountdown != null)
            StopCoroutine(_timerCountdown);
        //var rt = GameRoundHandler.Instance;

        if (FinalRound())
        {
            var totalWins = $"{CurrentWinsOf(Player.one)} to {CurrentWinsOf(Player.two)}";
        }
    }
}

public enum Player
{
    one = 0,
    two = 1
}
