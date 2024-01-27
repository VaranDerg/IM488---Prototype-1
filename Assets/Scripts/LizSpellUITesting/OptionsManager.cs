using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Liz
/// Description: Holds basic game options for easy editor use and future options screen?
/// </summary>
public class OptionsManager : MonoBehaviour
{
    [Header("Gameplay Options")]
    [SerializeField] [Range(3, 5)] private int _pointsToWin;
    [SerializeField] [Range(3, 5)] private int _spellOptionsGiven;

    [Header("Player Options")]
    [SerializeField] private bool _playersRegenerateHP;

    //Simple getters

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