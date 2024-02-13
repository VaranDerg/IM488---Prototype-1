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

    //This can be used to link the wheel to the player
    [Header("Temporary")]
    [SerializeField] private int _player;

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
            Debug.LogWarning($"An HP Wheel reads from values 0 to 1. The value ({amount}) is out of this range.");
        }

        _hpWheelSlider.value = amount;
    }

    public int GetPlayer()
    {
        return _player;
    }
}
