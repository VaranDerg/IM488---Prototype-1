using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    Dictionary<PlayerTag, PlayerManager> players;

    private void Start()
    {
        Instance = this;
    }

    public void AssignPlayer(PlayerTag tag, PlayerManager player)
    {
        players.Add(tag, player);
    }
}
