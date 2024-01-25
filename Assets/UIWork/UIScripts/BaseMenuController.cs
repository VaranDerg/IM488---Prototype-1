using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Liz
/// The BaseMenuController contains raw, basic functionality for very simple UI usage. It holds basic functions that all menus should be able to perform.
/// </summary>
public class BaseMenuController : MonoBehaviour
{
    //Include every menu "page" in this list.
    [SerializeField] protected List<GameObject> MenuPages;

    protected virtual void Awake()
    {
        if (MenuPages.Count > 0)
        {
            SetPageActive(MenuPages[0]);
        }
    }

    /// <summary>
    /// Pass in a page via the inspector. Alternatively, reference the "MenuPages" list.
    /// </summary>
    /// <param name="page">This page will be enabled.</param>
    public void SetPageActive(GameObject page)
    {
        foreach (GameObject p in MenuPages)
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
}
