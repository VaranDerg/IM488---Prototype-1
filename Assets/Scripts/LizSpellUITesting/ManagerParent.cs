using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerParent : MonoBehaviour
{
    public static ManagerParent Instance;
    [SerializeField] private List<GameObject> _managerPrefabs;

    [HideInInspector] public OptionsManager Options;
    [HideInInspector] public SpellManager Spells;
    [HideInInspector] public GameManager Game;

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

    private void SpawnManagers()
    {
        foreach (GameObject manager in _managerPrefabs)
        {
            Instantiate(manager, transform);
        }
    }

    private void AssignManagers()
    {
        Options = FindObjectOfType<OptionsManager>();
        Spells = FindObjectOfType<SpellManager>();
        Game = FindObjectOfType<GameManager>();
    }
}