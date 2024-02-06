using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElementalStats : MonoBehaviour
{
    [SerializeField]
    ElementalStatsSO elementalStatsSO;

    [SerializeField]
    List<AssociatedScalingStat> baseStats;

    Dictionary<ScalableStat, float> playerStats = new();

    bool hasStatsBeenAdded = false;

    private void Awake()
    {
        foreach (AssociatedScalingStat s in baseStats)
            playerStats[s.GetStat()] = s.GetScalingValue();
    }

    public void Initialize()
    {
        Player player = GetComponent<PlayerManager>().PlayerTag;
        List<TestSpellSO> spellList = ManagerParent.Instance.Spells.GetSpellListFromPlayer(player);

        foreach (TestSpellSO spell in spellList)
            AddElementStat(spell.SpellElement.element);
    }

    /// <summary>
    /// Adds an element's appropriate modifier to player's base stats
    /// </summary>
    /// <param name="element">Element type which decides what stat is changed</param>
    public void AddElementStat(Elements.SpellElement element)
    {
        if (hasStatsBeenAdded)
            return;

        //Debug.Log("Adding elemental stats for " + gameObject.name);
        foreach (ScalableStat stat in elementalStatsSO.GetElementStats(element))
        {
            playerStats[stat] += elementalStatsSO.GetStatScalar(stat);
            //Debug.Log("" + stat + ": " + playerStats[stat]);
        }
        hasStatsBeenAdded = true;
    }

    public float GetStat(ScalableStat stat)
    {
        //Debug.Log("Getting elemental stat for " + gameObject.name + " | Stat: " + stat);
        return playerStats[stat];
    }
}
