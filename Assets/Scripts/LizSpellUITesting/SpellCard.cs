using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _icon;

    private TestSpellSO _thisSpell;
    private int _playerSelecting;
    private SpellSelectUI _ui;

    private void Start()
    {
        
    }

    public void GiveSpell(TestSpellSO spell, int player)
    {
        _thisSpell = spell;
        _playerSelecting = player;

        _nameText.text = spell.SpellName;
        _descriptionText.text = spell.SpellDescription;
        _icon.color = spell.SpellColor;
    }

    public void SelectSpell()
    {
        PlayerSpellManager psm = PlayerSpellManager.Instance;
        psm.AddSpellToPlayer(_playerSelecting, _thisSpell);
    }
}