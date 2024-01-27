using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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