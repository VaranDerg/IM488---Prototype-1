using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Animator _animator;

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

        _nameText.text = spell.SpellName;
        _descriptionText.text = spell.SpellDescription;

        GetComponentInChildren<SpellIcon>().SetUpIcon(spell);
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

        ManagerParent.Instance.Audio.PlaySoundEffect(_thisSpell.SpellElement.SoundEffect);
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