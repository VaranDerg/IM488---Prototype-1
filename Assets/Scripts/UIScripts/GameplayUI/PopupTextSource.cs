using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextSource : MonoBehaviour
{
    [SerializeField] private GameObject _popupTextPrefab;

    public void DisplayPopup(string text, Color color, float lifetime)
    {
        GameObject thisTextPopup = Instantiate(_popupTextPrefab, transform.position, Quaternion.identity);
        PopupTextUI ptu = thisTextPopup.GetComponent<PopupTextUI>();
        thisTextPopup.transform.SetParent(null);

        ptu.PreparePopupText(text, color, lifetime);
    }
}
