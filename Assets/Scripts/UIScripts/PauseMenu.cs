using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Author: Liz
/// Description: Provides pause-menu specific behavior. 
/// </summary>
public class PauseMenu : BaseMenuController
{
    [SerializeField] private List<GameObject> _pauseObjects;

    private bool _gamePaused;

    /// <summary>
    /// Disables this specific menu
    /// </summary>
    protected override void Awake()
    {
        DisablePages();
        SetPauseObjectsActive(false);
        SetTimescale(1f);
        _gamePaused = false;
    }

    /// <summary>
    /// Checks for pause inputs. Change to new input system upon implementation occuring. 
    /// </summary>
    private void Update()
    {
        if (SceneTransitions.TransitionActive)
        {
            return;
        }

        bool controllerPaused = false;
        foreach(Gamepad g in Gamepad.all)
        {
            if(g != null && (g.selectButton.wasPressedThisFrame || g.startButton.wasPressedThisFrame))
            {
                controllerPaused = true;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || controllerPaused)
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Toggles the paused state.
    /// </summary>
    public void TogglePause()
    {
        if (!_gamePaused)
        {
            SetPauseObjectsActive(true);
            SetTimescale(0f);
            _gamePaused = true;

            SetPageActive(MenuPages[0]);
        }
        else
        {
            SetPauseObjectsActive(false);
            SetTimescale(1f);
            _gamePaused = false;

            DisablePages();
        }
    }

    /// <summary>
    /// Loads the main menu while resetting the game.
    /// </summary>
    public void ToMainMenu()
    {
        StopAllCoroutines();

        ManagerParent.Instance.Game.ResetGame();

        LoadScene(ManagerParent.Instance.Game.GetMainMenuScene());
    }

    /// <summary>
    /// Enables/disables objects for the pause menu
    /// </summary>
    /// <param name="active">True if you want the objects enabled</param>
    private void SetPauseObjectsActive(bool active)
    {
        foreach (GameObject obj in _pauseObjects)
        {
            obj.SetActive(active);
        }
    }

    /// <summary>
    /// Disables every pause page
    /// </summary>
    private void DisablePages()
    {
        foreach (GameObject page in MenuPages)
        {
            page.SetActive(false);
        }
    }
}
