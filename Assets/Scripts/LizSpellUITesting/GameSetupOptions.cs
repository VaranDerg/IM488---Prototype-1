using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupOptions : MonoBehaviour
{
    [Header("Gameplay Information")]
    [SerializeField] [Range(3, 5)] private int _pointsToWin;
    [SerializeField] [Range(3, 5)] private int _spellOptionsGiven;
    [SerializeField] private List<TestSpellSO> _allSpells = new List<TestSpellSO>();

    [Header("Player Information")]
    [SerializeField] private bool _playersRegenerateHP;

    public int GetPointToWin()
    {
        return _pointsToWin;
    }

    public int GetSpellOptionsGive()
    {
        return _spellOptionsGiven;
    }

    public List<TestSpellSO> GetAllSpells()
    {
        return _allSpells;
    }

    public bool GetPlayersRegenerateHP()
    {
        return _playersRegenerateHP;
    }
}