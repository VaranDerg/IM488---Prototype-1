using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellManager : MonoBehaviour
{
    public static PlayerSpellManager Instance;

    [SerializeField] private List<TestSpellSO> _allSpells = new List<TestSpellSO>();
    private List<TestSpellSO> _playerOneSpells, _playerTwoSpells;
    private SpellSelectionMode _currentSelectionMode;

    public enum SpellSelectionMode
    {
        PlayerOne,
        PlayerTwo,
        BothPlayers
    }

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
    /// Sets the spell selection mode.
    /// </summary>
    /// <param name="mode"></param>
    public void PrepareSpellSelectionState(SpellSelectionMode mode)
    {
        _currentSelectionMode = mode;

        switch (mode)
        {
            case SpellSelectionMode.PlayerOne:
                //Generate spells for player one in UI
                break;
            case SpellSelectionMode.PlayerTwo:
                //Generate spells for player two in UI
                break;
            case SpellSelectionMode.BothPlayers:
                //Generate spells for player one in UI,
                break;
        }
    }

    public void LoadSceneAfterSpellPicked()
    {
        if (_currentSelectionMode == SpellSelectionMode.BothPlayers)
        {
            //Load spell select for player two
        }
        else
        {
            //Load random arena
        }
    }

    /// <summary>
    /// Gets spells that a player has not selected.
    /// </summary>
    /// <param name="player">The player's spells to check</param>
    /// <returns>A list of spell that player doesn't have.</returns>
    public List<TestSpellSO> GetNewSpellsForPlayer(int player)
    {
        List<TestSpellSO> newSpells = new List<TestSpellSO>();

        if (PlayerIsValid(player))
        {
            if (player == 1)
            {
                foreach (TestSpellSO spell in _allSpells)
                {
                    if (!_playerOneSpells.Contains(spell))
                    {
                        newSpells.Add(spell);
                    }
                }
            }
            else
            {
                foreach (TestSpellSO spell in _allSpells)
                {
                    if (!_playerTwoSpells.Contains(spell))
                    {
                        newSpells.Add(spell);
                    }
                }
            }
        }

        return newSpells;
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

    public int SpellSelectionModeToPlayer(SpellSelectionMode mode)
    {
        int player = 0;

        switch(mode)
        {
            case SpellSelectionMode.PlayerOne:
                player = 1;
                break;
            case SpellSelectionMode.PlayerTwo:
                player = 2;
                break;
            case SpellSelectionMode.BothPlayers:
                //If you set this to "BothPlayers, it will default to 1. After player one picks, the SpellSelectionMode will be set to "PlayerTwo."
                player = 1;
                break;
        }

        return player;
    }

    public List<TestSpellSO> GetPlayerOneSpells()
    {
        return _playerOneSpells;
    }

    public List<TestSpellSO> GetPlayerTwoSpells()
    {
        return _playerTwoSpells;
    }

    public SpellSelectionMode GetCurrentSpellSelectionMode()
    {
        return _currentSelectionMode;
    }
}
