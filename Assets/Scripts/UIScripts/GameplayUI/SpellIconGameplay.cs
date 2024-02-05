using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellIconGameplay : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _iconBorder;
    [SerializeField] private Image _barColor;
    [SerializeField] private Image _barInverseFill;
    private TestSpellSO _thisSpell;

    /// <summary>
    /// Prepares a spell icon with correct values.
    /// </summary>
    /// <param name="spell">The testspellso used.</param>
    /// <returns>Returns the icon to easily reference its progress bar.</returns>
    public SpellIconGameplay PrepareSpellIcon(TestSpellSO spell)
    {
        _icon.sprite = spell.SpellType.SpellTypeIcon;
        //temp
        _icon.color = spell.SpellElement.ElementColor;
        //endtempt
        _iconBorder.color = spell.SpellElement.ElementColor;
        _barColor.color = spell.SpellElement.ElementColor;

        _thisSpell = spell;

        return this;
    }

    /// <summary>
    /// Sets the progress bar's value. The UI elements are inverted, so a value of "0" would fully fill the bar, as its cooldown has completed.
    /// </summary>
    /// <param name="valueNormalized">A normalized value of the time remaining before the spell activates.</param>
    public void SetProgressBar(float valueNormalized)
    {
        if (valueNormalized > 1 || valueNormalized < 0)
        {
            Debug.LogWarning($"Passed value {valueNormalized} is not normalized!");
        }

        _barInverseFill.fillAmount = valueNormalized;
    }

    public TestSpellSO GetTestSpellSO()
    {
        return _thisSpell;
    }
}
