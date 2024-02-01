using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalStats : MonoBehaviour
{
    [Header("Base Values")]
    [SerializeField]
    float baseSpeed;
    [SerializeField]
    float baseAttack;
    [SerializeField]
    float baseArea;

    [Header("Added Values")]
    [SerializeField]
    float speedToAdd;
    [SerializeField]
    float attackToAdd;
    [SerializeField]
    float areaToAdd;

    // Player's actual current stats
    private float speed, attack, area;

    private void Start()
    {
        speed = baseSpeed;
        attack = baseAttack;
        area = baseArea;
    }

    /// <summary>
    /// Adds an element's appropriate modifier to player's base stats
    /// </summary>
    /// <param name="element">Element type which decides what stat is changed</param>
    public void AddElementStat(Elements.SpellElement element)
    {
        switch (element)
        {
            case (Elements.SpellElement.Fire):
                attack += attackToAdd;
                return;

            case (Elements.SpellElement.Ice):
                area += areaToAdd;
                return;

            case (Elements.SpellElement.Lightning):
                speed += speedToAdd;
                return;

            default:
                return;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetAttack()
    {
        return attack;
    }

    public float GetArea()
    {
        return area;
    }
}
