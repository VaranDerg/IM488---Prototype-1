using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    [Header("Gameplay Options")]
    [SerializeField] [Range(3, 5)] private int _pointsToWin;
    [SerializeField] [Range(3, 5)] private int _spellOptionsGiven;

    [Header("Player Options")]
    [SerializeField] private bool _playersRegenerateHP;

    public int GetSpellsToPick()
    {
        return _spellOptionsGiven;
    }

    public int GetPointsToWin()
    {
        return _pointsToWin;
    }

    public bool GetPlayersRegenerateHP()
    {
        return _playersRegenerateHP;
    }
}