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

    private void Start()
    {
        PopulateWithPlayerSpellInformation();
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
            SpellIcon icon = Instantiate(_spellIcon, _spellIconGrid).GetComponent<SpellIcon>();
            icon.SetUpIcon(spell);
        }
    }
}