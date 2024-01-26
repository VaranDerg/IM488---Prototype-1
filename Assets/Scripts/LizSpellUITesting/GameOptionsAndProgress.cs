using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptionsAndProgress : MonoBehaviour
{
    public static int PlayerOneScore;
    public static int PlayerTwoScore;

    [Header("Gameplay Information")]
    [SerializeField] [Range(3, 5)] private int _pointsToWin;

    [Header("Player Information")]
    [SerializeField] private bool _playersRegenerateHP;

    public int GetPointToWin()
    {
        return _pointsToWin;
    }

    public bool GetPlayersRegenerateHP()
    {
        return _playersRegenerateHP;
    }
}