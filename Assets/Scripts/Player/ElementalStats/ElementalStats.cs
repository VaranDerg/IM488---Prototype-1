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

    /// <summary>
    /// Call this to add / remove temporary stats
    /// </summary>
    /// <param name="stat">Stat type</param>
    /// <param name="amt">amount to be changed. Can be negative to remove</param>
    public void AddStat(ScalableStat stat, float amt)
    {
        playerStats[stat] += amt;
    }

    /// <summary>
    /// Adds a stat for a set period of time
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="amt"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public IEnumerator TemporaryAddStat(ScalableStat stat,float amt,float duration)
    {
        Debug.Log("Stat: " + stat + " amount: " + amt + " duration: " + duration);
        AddStat(stat, amt);
        yield return new WaitForSeconds(duration);
        Debug.Log(playerStats[stat]);
        AddStat(stat, -amt);
    }

    public float GetStat(ScalableStat stat)
    {
        //Debug.Log("Getting elemental stat for " + gameObject.name + " | Stat: " + stat + " | Value : " + playerStats[stat]);
        return playerStats[stat];
    }

    public void LogStats()
    {
        foreach (ScalableStat stat in playerStats.Keys)
            Debug.Log(stat + ": " + playerStats[stat]);
    }
}
