using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TestSpellSO : ScriptableObject
{
    public string SpellName;
    [TextArea(3, 10)] public string SpellDescription;
    public Color SpellColor;
    public SpellType TypeOfSpell;

    public enum SpellType
    {
        Projectile,
        Object,
        Dash,
        AreaOfEffect
    }
}
