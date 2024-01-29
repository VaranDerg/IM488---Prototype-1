using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Liz
/// Description: Spell icon functionality, present on Spell Cards and the Spell Box Gameplay
/// </summary>
public class SpellIcon : BaseUIElement
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _border;

    /// <summary>
    /// Sets up this icon's visuals.
    /// </summary>
    /// <param name="spell">Spell information object</param>
    public void SetUpIcon(TestSpellSO spell)
    {
        _icon.color = Color.gray;
        _border.color = spell.SpellElement.ElementColor;
    }
}