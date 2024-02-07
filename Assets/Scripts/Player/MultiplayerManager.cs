using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    Dictionary<Player, PlayerManager> players = new();
    Dictionary<Player, SpellBoxGameplay> spellboxUI = new();

    [SerializeField]
    ElementalStatsSO elementalStatsContainer;

    bool p1Assigned = false;

    bool p2Assigned = false;

    private void OnEnable()
    {
        Instance = this;
    }

    public void AssignPlayer(Player tag, PlayerManager player)
    {
        if (p1Assigned && p2Assigned)
            return;

        players.Add(tag, player);
        switch(tag){
            case (Player.one):
                p1Assigned = true;
                break;
            case (Player.two):
                p2Assigned = true;
                break;
        }

        if (p1Assigned && p2Assigned)
        {
            InitializeElementalStats();

            //ManagerParent.Instance.Spells.AssignStarterSpellToPlayers();
        }
            

    }

    public void AssignSpellbox(Player player, SpellBoxGameplay spellbox)
    {
        spellboxUI[player] = spellbox;
    }

    public SpellBoxGameplay GetPlayerSpellbox(Player player)
    {
        return spellboxUI[player];
    }

    private void InitializeElementalStats()
    {
        elementalStatsContainer.Initialize();

        players[Player.one].GetElementalStats().Initialize();
        players[Player.two].GetElementalStats().Initialize();
    }

    public PlayerManager GetPlayer(Player tag)
    {
        return players[tag];
    }

    public PlayerManager GetOpposingPlayer(Player tag)
    {
        foreach (Player t in players.Keys)
            if (t != tag)
                return players[t];

        return null;
    }
}
