using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Liz
/// Description: Holds every manager for easy editor use
/// </summary>
public class ManagerParent : MonoBehaviour
{
    public static ManagerParent Instance;
    [SerializeField] private List<GameObject> _managerPrefabs;

    [HideInInspector] public OptionsManager Options;
    [HideInInspector] public SpellManager Spells;
    [HideInInspector] public GameManager Game;
    [HideInInspector] public AudioManager Audio;

    /// <summary>
    /// Singleton pattern
    /// </summary>
    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        SpawnManagers();
        AssignManagers();
    }

    /// <summary>
    /// Spawns every manager
    /// </summary>
    private void SpawnManagers()
    {
        foreach (GameObject manager in _managerPrefabs)
        {
            Instantiate(manager, transform);
        }
    }

    /// <summary>
    /// Assigns every manager
    /// </summary>
    private void AssignManagers()
    {
        Options = FindObjectOfType<OptionsManager>();
        Spells = FindObjectOfType<SpellManager>();
        Game = FindObjectOfType<GameManager>();
        Audio = FindObjectOfType<AudioManager>();
    }
}