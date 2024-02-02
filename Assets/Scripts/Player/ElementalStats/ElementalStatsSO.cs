using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class ElementalStatsSO : ScriptableObject
{
    [SerializeField]
    List<AssociatedElement> associatedElements;

    [SerializeField]
    List<AssociatedScalingStat> associatedStats;

    List<ScalableStat> scalableStats = ScalableStat.GetValues(typeof(ScalableStat)).OfType<ScalableStat>().ToList();

    Dictionary<Elements.SpellElement, TestElementSO> elements = new();

    Dictionary<ScalableStat, float> statScalars = new();

    private void Awake()
    {
        foreach (AssociatedElement a in associatedElements)
            elements[a.GetElement()] = a.GetSO();

        foreach (AssociatedScalingStat s in associatedStats)
            statScalars[s.GetStat()] = s.GetScalingValue();
    }

    /// <summary>
    /// Gets an elements' associated scaling stats
    /// </summary>
    /// <param name="element">The associated element</param>
    public List<ScalableStat> GetElementStats(Elements.SpellElement element)
    {
        return elements[element].scalingStats;
    }

    public float GetStatScalar(ScalableStat stat)
    {
        return statScalars[stat];
    }

    // Enable this if its functionality is desired
    /// <summary>
    /// Gets a list of all ScalableStat values (because C# doesn't have a .GetValues() method for some reason)
    /// </summary>
    /*public List<ScalableStat> GetScalableStatsList()
    {
        return scalableStats;
    }*/

    /*public TestElementSO GetElementSO(Elements.SpellElement element)
    {
        return elements[element];
    }*/
}
public enum ScalableStat
{
    DAMAGE,
    MOVE_SPEED,
    COOLDOWN_RATE,
    PROJECTILE_SPEED,
    PROJECTILE_SIZE
}

[System.Serializable]
public class AssociatedElement
{
    [SerializeField]
    string name;

    [Space]

    [SerializeField]
    Elements.SpellElement element;

    [SerializeField]
    TestElementSO elementSO;

    public Elements.SpellElement GetElement()
    {
        return element;
    }

    public TestElementSO GetSO()
    {
        return elementSO;
    }
}

[System.Serializable] 
public class AssociatedScalingStat
{
    [SerializeField]
    string name;

    [Space]

    [SerializeField]
    ScalableStat stat;

    [SerializeField]
    float scalingValue;

    public ScalableStat GetStat()
    {
        return stat;
    }

    public float GetScalingValue()
    {
        return scalingValue;
    }
}