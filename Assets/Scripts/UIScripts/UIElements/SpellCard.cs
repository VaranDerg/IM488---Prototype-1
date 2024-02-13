using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Author: Liz
/// Description: Behavior for a spell card on the spell select screen.
/// </summary>
public class SpellCard : BaseUIElement
{
    private const string SELECT_ANIM_NAME = "SpellCardSelect";
    private const string REMOVE_ANIM_NAME = "SpellCardExit";

    [SerializeField] private Image _spellIcon;
    [SerializeField] private Image _elementIcon;
    [SerializeField] private Image _iconBorder;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _statBoostTest;
    [Space]
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _button;

    private TestSpellSO _thisSpell;
    private int _playerSelecting;

    /// <summary>
    /// Gives this spell card a spell
    /// </summary>
    /// <param name="spell">Spell information object</param>
    /// <param name="player">The player who is selecting the spell</param>
    public void GiveSpell(TestSpellSO spell, int player)
    {
        _thisSpell = spell;
        _playerSelecting = player;

        _spellIcon.sprite = spell.SpellIcon;
        _elementIcon.sprite = spell.SpellElement.ElementIcon;
        _iconBorder.color = spell.SpellElement.ElementColor;

        _nameText.text = spell.SpellName;
        _descriptionText.text = spell.SpellDescription;
        _statBoostTest.text = spell.SpellElement.ScalingStatName;

        ColorBlock colorBlock = _button.colors;
        colorBlock.selectedColor = spell.SpellElement.ElementColor;
        colorBlock.highlightedColor = spell.SpellElement.ElementColor;
        _button.colors = colorBlock;
    }

    /// <summary>
    /// Adds the spell to the player and plays an animation.
    /// </summary>
    public void SelectSpell()
    {
        GetComponent<Button>().enabled = false;

        SpellManager psm = ManagerParent.Instance.Spells;
        psm.AddSpellToPlayer(_playerSelecting, _thisSpell);

        FindObjectOfType<SpellSelectUI>().RemovePassedSpellCards(this);

        ManagerParent.Instance.Audio.PlaySoundEffect(_thisSpell.SpellElement.SoundEffectName);
        _animator.Play(SELECT_ANIM_NAME);
    }

    /// <summary>
    /// Removes this card visually and disables the button.
    /// </summary>
    public void RemoveCard()
    {
        GetComponent<Button>().enabled = false;
        _animator.Play(REMOVE_ANIM_NAME);
    }
}