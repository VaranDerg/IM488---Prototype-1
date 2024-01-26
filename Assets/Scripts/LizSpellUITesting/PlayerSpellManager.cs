using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellManager : MonoBehaviour
{
    public static PlayerSpellManager Instance;

    private List<TestSpellSO> _playerOneSpells, _playerTwoSpells;

    /// <summary>
    /// Singleton pattern
    /// </summary>
    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Adds a spell to that player's list
    /// </summary>
    /// <param name="player">The player to add a spell to</param>
    /// <param name="spell">The spell data object</param>
    public void AddSpellToPlayer(int player, TestSpellSO spell)
    {
        if (PlayerIsValid(player))
        {
            if (player == 1)
            {
                _playerOneSpells.Add(spell);
            }
            else
            {
                _playerTwoSpells.Add(spell);
            }
        }
    }

    /// <summary>
    /// Determines if a player already has a spell
    /// </summary>
    /// <param name="player">The player to add a spell to</param>
    /// <param name="spell">The spell data object</param>
    /// <returns>True if that player's list contains a matching spell. Otherwise returns false.</returns>
    public bool PlayerHasSpell(int player, TestSpellSO spell)
    {
        if (PlayerIsValid(player))
        {
            if (player == 1)
            {
                foreach (TestSpellSO testSpellSO in _playerOneSpells)
                {
                    if (testSpellSO.SpellName == spell.SpellName)
                    {
                        return true;
                    }
                }
            }
            else
            {
                foreach (TestSpellSO testSpellSO in _playerTwoSpells)
                {
                    if (testSpellSO.SpellName == spell.SpellName)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Checks is player entry is valid
    /// </summary>
    /// <param name="player">The player being interacted with</param>
    /// <returns>True if 1 or 2. False otherwise, and throws an exception.</returns>
    private bool PlayerIsValid(int player)
    {
        if (player > 2 || player < 1)
        {
            Debug.LogError($"Player {player} does not exist!");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Clears both player spell lists for a new game.
    /// </summary>
    public void ClearSpellsForBothPlayers()
    {
        _playerOneSpells.Clear();
        _playerTwoSpells.Clear();
    }
}
