using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Liz
/// The MenuController contains raw, basic functionality for very simple UI usage. It holds basic functions that all menus should be able to perform.
/// </summary>
public class MenuController : MonoBehaviour
{
    //Include every menu "page" in this list.
    [SerializeField] private List<GameObject> _menuPages;

    private void Start()
    {
        SetPageActive(_menuPages[0]);
    }

    /// <summary>
    /// Pass in a page via the inspector. Alternatively, reference the "_menuPages" list.
    /// </summary>
    /// <param name="page">This page will be enabled.</param>
    public void SetPageActive(GameObject page)
    {
        foreach (GameObject p in _menuPages)
        {
            p.SetActive(false);
        }

        page.SetActive(true);
    }

    /// <summary>
    /// Will load a scene in the build settings.
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Closes the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
