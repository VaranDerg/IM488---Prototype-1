using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellCard : BaseUIElement
{
    private const string SELECT_ANIM_NAME = "SpellCardSelect";
    private const string REMOVE_ANIM_NAME = "SpellCardExit";

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Animator _animator;

    private TestSpellSO _thisSpell;
    private int _playerSelecting;

    public void GiveSpell(TestSpellSO spell, int player)
    {
        _thisSpell = spell;
        _playerSelecting = player;

        _nameText.text = spell.SpellName;
        _descriptionText.text = spell.SpellDescription;

        GetComponentInChildren<SpellIcon>().SetUpIcon(spell);
    }

    public void SelectSpell()
    {
        SpellManager psm = ManagerParent.Instance.Spells;
        psm.AddSpellToPlayer(_playerSelecting, _thisSpell);

        FindObjectOfType<SpellSelectUI>().RemovePassedSpellCards(this);

        _animator.Play(SELECT_ANIM_NAME);
    }

    public void RemoveCard()
    {
        GetComponent<Button>().enabled = false;
        _animator.Play(REMOVE_ANIM_NAME);
    }
}