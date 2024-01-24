using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoundHandler : MonoBehaviour
{
    [SerializeField] private float _roundTimerLength;
    [SerializeField] private Vector3 p1StartLoc;
    [SerializeField] private Vector3 p2StartLoc;
    private const int _winsRequired = 3;
    private float _currentRoundTime;
    int _p1Wins, _p2Wins;
    private Coroutine _timerCountdown;

    internal GameObject P1;
    internal GameObject P2;

    public enum Player
    {
        one = 0,
        two = 1
    }
    static public GameRoundHandler Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
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

    void RoundStart() => _timerCountdown = StartCoroutine(RoundTimerCountDown());
    
    void Awake() => Reset();
    void OnEnable() => Instance = this;
    void OnDisable() => Instance = null;
    void OnDestroy() => OnDisable();

    public void Reset()
    {
        _p1Wins = _p2Wins = 0;
    }

    public void P1Won()
    {

    }

    public void P2Won()
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

    public void RecordRoundWinner(Player winner) => setWins(winner, getWins(winner) + 1);

    public int CurrentWinsOf(Player player) => getWins(player);

    public bool FinalRound() => _p1Wins == _winsRequired || _p2Wins == _winsRequired;

    public Player FinalWinner => _p1Wins > _p2Wins ? Player.one : Player.two;

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

        var rt = GameRoundHandler.Instance;

        GameRoundHandler.Instance.RecordRoundWinner(winner: GameRoundHandler.Player.one);

        if (rt.FinalRound())
        {
            var totalWins = $"{CurrentWinsOf(GameRoundHandler.Player.one)} to {CurrentWinsOf(GameRoundHandler.Player.two)}";
        }
    }
}
