using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : BaseMenuController
{
    [SerializeField] private List<GameObject> _pauseObjects;

    private bool _gamePaused;

    protected override void Awake()
    {
        SetPauseObjectsActive(false);
        SetTimescale(1f);
        _gamePaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

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

    private void SetPauseObjectsActive(bool active)
    {
        foreach (GameObject obj in _pauseObjects)
        {
            obj.SetActive(active);
        }
    }

    private void DisablePages()
    {
        foreach (GameObject page in MenuPages)
        {
            page.SetActive(false);
        }
    }

    private void SetTimescale(float newTimescale)
    {
        Time.timeScale = newTimescale;
    }
}
