using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Author: Liz
/// Description: Controls the top left and right spell boxes during gameplay
/// </summary>
public class SpellBoxGameplay : BaseUIElement
{
    [SerializeField] [Range(1, 2)] private int _whichPlayer;
    [Space]
    [SerializeField] private TextMeshProUGUI _playerText;
    [SerializeField] private GameObject _spellIcon;
    [SerializeField] private Transform _spellIconGrid;
    private List<SpellIconGameplay> _activeIcons = new();

    private void Start()
    {
        PopulateWithPlayerSpellInformation();

        MultiplayerManager.Instance.AssignSpellbox(ConvertToPlayer(), this);
    }

    /// <summary>
    /// Sets up the gameplay boxes with the player's current spells
    /// </summary>
    private void PopulateWithPlayerSpellInformation()
    {
        _playerText.text = ManagerParent.Instance.Game.GetPlayerName() + " " + _whichPlayer;

        SpellManager psm = ManagerParent.Instance.Spells;
        List<TestSpellSO> playerSpells;
        if (_whichPlayer == 1)
        {
            playerSpells = psm.GetPlayerOneSpells();
        }
        else
        {
            playerSpells = psm.GetPlayerTwoSpells();
        }

        foreach (TestSpellSO spell in playerSpells)
        {
            if (spell.ExcludeFromUI)
                continue;

            SpellIconGameplay icon = Instantiate(_spellIcon, _spellIconGrid).GetComponent<SpellIconGameplay>();
            icon.PrepareSpellIcon(spell);

            if (_activeIcons == null)
                Debug.Log(spell.SpellName);

            _activeIcons.Add(icon);
        }
    }

    /// <summary>
    /// Converts _whichPlayer to a Player enum value
    /// </summary>
    /// <returns>Player enum value</returns>
    private Player ConvertToPlayer()
    {
        return _whichPlayer == 1 ? Player.one : Player.two;
    }

    /// <summary>
    /// Updates the given spell icon based on the TestSpellSO given.
    /// </summary>
    /// <param name="spell">The spell you want to search for on the player ui.</param>
    /// <param name="valueNormalized">A value, 0 - 1, of the cooldown progress.</param>
    public void UpdateSpellIconBar(TestSpellSO spell, float valueNormalized)
    {
        SpellIconGameplay thisIcon = GetSpellIconFromTestSpellSO(spell);

        if (thisIcon == null)
        {
            Debug.LogWarning($"Player {_whichPlayer} does not have the spell: {spell.SpellName}.");
            return;
        }

        thisIcon.SetProgressBar(valueNormalized);
    }

    /// <summary>
    /// Gets a SpellIconGameplay from the list _activeIcons.
    /// </summary>
    /// <param name="spell">The spell you're searching for.</param>
    /// <returns>The SpellIconGameplay matching the spell you're looking for.</returns>
    private SpellIconGameplay GetSpellIconFromTestSpellSO(TestSpellSO spell)
    {
        foreach (SpellIconGameplay icon in _activeIcons)
        {
            if (icon.GetTestSpellSO() == spell)
            {
                return icon;
            }
        }

        return null;
    }
}