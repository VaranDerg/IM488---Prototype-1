using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellIcon : BaseUIElement
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _border;

    public void SetUpIcon(TestSpellSO spell)
    {
        _icon.color = Color.gray;
        _border.color = spell.SpellColor;
    }
}