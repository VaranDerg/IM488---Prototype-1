using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    private Outline _playerOutline;

    private void Start()
    {
        AddOutline(gameObject.transform.parent.gameObject);
    }
    public void AddOutline(GameObject objToApplyTo)
    {
        if (GetComponentInParent<PlayerManager>() == null)
            return;

        if (MultiplayerManager.Instance == null)
            return;

        _playerOutline = objToApplyTo.AddComponent<Outline>();
        //MultiplayerManager.Instance.GetPlayer(GetComponentInParent<PlayerManager>().PlayerTag))
        try
        {
            _playerOutline.OutlineColor = MultiplayerManager.Instance.GetColorFromPlayer(GetComponentInParent<PlayerManager>().PlayerTag);
            _playerOutline.OutlineWidth = MultiplayerManager.Instance.GetOutlineSize();
        }
        catch
        {
            
        }


    }

    public Outline GetOutline()
    {
        return _playerOutline;
    }
}
