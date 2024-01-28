using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Liz
/// Description: Holds basic spell information. You may want to convert this script instead of writing a new one.
/// </summary>
[CreateAssetMenu()]
public class TestSpellSO : ScriptableObject
{
    public string SpellName;
    [TextArea(3, 10)] public string SpellDescription;
    public Color SpellColor;
    public SpellType TypeOfSpell;
    public GameObject AssociatedPrefab;

    //Currently unused, but should be used for visuals 
    public enum SpellType
    {
        Projectile,
        Object,
        Dash,
        AreaOfEffect
    }
}
