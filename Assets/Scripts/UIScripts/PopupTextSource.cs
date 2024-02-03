using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextSource : MonoBehaviour
{
    [SerializeField] private GameObject _popupTextPrefab;

    public void DisplayPopup(string text, Color color, float lifetime)
    {
        GameObject thisTextPopup = Instantiate(_popupTextPrefab, transform);
        PopupTextUI ptu = thisTextPopup.GetComponent<PopupTextUI>();
        thisTextPopup.transform.SetParent(null);

        ptu.PreparePopupText(text, color, lifetime);
    }

    public void PopupTextExample()
    {
        GameObject thisTextPopup = Instantiate(_popupTextPrefab, transform);
        PopupTextUI ptu = thisTextPopup.GetComponent<PopupTextUI>();
        thisTextPopup.transform.SetParent(null);

        ptu.PreparePopupText("+Stat!", Color.green, 1.5f);
    }
}
