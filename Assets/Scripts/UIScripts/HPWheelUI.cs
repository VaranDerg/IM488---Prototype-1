using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Extremel
/// </summary>
public class HPWheelUI : BaseUIElement
{
    [SerializeField] private Slider _hpWheelSlider;

    private void Start()
    {
        _hpWheelSlider.maxValue = 1f;
        _hpWheelSlider.value = 1f;
    }

    /// <summary>
    /// Sets the value of the HP Wheel.
    /// </summary>
    /// <param name="amount">0 to 1. Pass currentHP/maxHP for a normalized value.</param>
    public void SetWheelValue(float amount)
    {
        if (amount > 1 || amount < 0)
        {
            Debug.LogWarning("An HP Wheel reads from values 0 to 1. Value passed was out of this range.");
        }

        _hpWheelSlider.value = amount;
    }
}
