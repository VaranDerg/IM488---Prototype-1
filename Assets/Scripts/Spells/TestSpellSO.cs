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
    [Space]
    public bool ExcludeFromUI;
    [Space]
    public TestElementSO SpellElement;
    public TestSpellTypeSO SpellType;
    [Space]
    [SerializeField] private GameObject AssociatedPrefab;

    public GameObject GetPrefab()
    {
        return AssociatedPrefab;
    }
}