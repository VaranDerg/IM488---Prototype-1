using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    Dictionary<Player, PlayerManager> players = new();

    private void OnEnable()
    {
        Instance = this;
    }

    public void AssignPlayer(Player tag, PlayerManager player)
    {
        players.Add(tag, player);
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
