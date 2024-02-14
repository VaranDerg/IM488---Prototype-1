using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartupMenu : BaseMenuController
{
    bool hasP1Joined = false;

    [SerializeField] private TextMeshProUGUI _waitingText1, _waitingText2;

    protected override void Awake()
    {
        
    }

    private void Start()
    {
        _waitingText1.color = MultiplayerManager.Instance.GetColorFromPlayer(Player.one);
        _waitingText2.color = MultiplayerManager.Instance.GetColorFromPlayer(Player.two);
    }

    /// <summary>
    /// Prompts player 2 to press a button.
    /// </summary>
    public void OpenNextPage()
    {
        SetPageActive(MenuPages[1]);
    }

    /// <summary>
    /// Opens the main menu.
    /// </summary>
    public void SetupComplete()
    {
        LoadScene(1);
    }

    public void OnPlayerJoined()
    {
        //Debug.Log("Player Joined");

        if (!hasP1Joined)
        {
            OpenNextPage();
            hasP1Joined = true;
        }
        else
        {
            //Debug.Log("Setup Done!");
            SetupComplete();
        }
    }
}