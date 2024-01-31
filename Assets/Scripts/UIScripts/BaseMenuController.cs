using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Liz
/// The BaseMenuController contains raw, basic functionality for very simple UI usage. It holds basic functions that all menus should be able to perform.
/// </summary>
public class BaseMenuController : MonoBehaviour
{
    //Include every menu "page" in this list.
    [SerializeField] protected List<GameObject> MenuPages;

    [SerializeField]
    GameObject firstSelected;

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

        if(firstSelected != null)
        {
            AssignFirstSelected(firstSelected);
        }
    }

    /// <summary>
    /// Pass in a UI object that should be selected
    /// </summary>
    /// <param name="obj">This object will be selected</param>
    public void AssignFirstSelected(GameObject obj)
    {
        InputParent.Instance._P1Input.SetSelected(obj, gameObject);
        InputParent.Instance._P2Input.SetSelected(obj, gameObject);
    }

    /// <summary>
    /// Will load a scene in the build settings.
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void LoadScene(int sceneIndex)
    {
        SetTimescale(1f);

        SceneTransitions.Instance.LoadSceneWithTransition(SceneTransitions.TransitionType.Fade, sceneIndex);
    }

    /// <summary>
    /// Adjusts the current timescale.
    /// </summary>
    /// <param name="newTimescale">The new timescale to set</param>
    public void SetTimescale(float newTimescale)
    {
        Time.timeScale = newTimescale;
    }
}
