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

    [Space]
    [SerializeField] private Color _p1OutlineColor;
    [SerializeField] private Color _p2OutlineColor;
    [SerializeField] private float _outlineSize;

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

    public PlasmoVisuals GetPlayerVisuals(Player tag)
    {
        return GetPlayer(tag).GetPlayerController().GetVisuals();
    }

    public PlayerManager GetOpposingPlayer(Player tag)
    {
        foreach (Player t in players.Keys)
            if (t != tag)
                return players[t];

        return null;
    }
    public Color GetColorFromPlayer(Player tag)
    {
        if(tag == Player.two)
        {
            Debug.Log("HEY");
        }
        switch (tag)
        {
            case (Player.one):
                return _p1OutlineColor;
            case (Player.two):
                return _p2OutlineColor;
        }
        return Color.white;
    }

    public float GetOutlineSize()
    {
        return _outlineSize;
    }
}
