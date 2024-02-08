using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupAbstract : MonoBehaviour, IPickup
{
    [SerializeField]
    PickupDataSO data;

    private void Start()
    {
        GetComponentInChildren<PickupCoinVisual>().PreparePickupCoinVisual(data);
    }

    private void OnTriggerEnter(Collider collision)
    {
        PlayerManager pm = collision.GetComponent<PlayerManager>();
        if (pm != null)
        {
            PickUpObject(pm);
            DisplayUIPickup(collision);
        }
    }

    protected abstract void PickUpObject(PlayerManager pm);

    public PickupDataSO GetScriptableObject()
    {
        return data;
    }

    public void DisplayUIPickup(Collider collision)
    {
        collision.GetComponent<PlayerManager>().SpawnText(data.PopupText, Color.green, 1.5f);
    }

    public void PostPickup()
    {
        PickupManager.Instance.RemovePickupFromLocation(gameObject);
        Destroy(gameObject);
    }
}
