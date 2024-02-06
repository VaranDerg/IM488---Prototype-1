using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupAbstract : MonoBehaviour, IPickup
{
    protected abstract void PickUpObject(PlayerManager pm);
    [SerializeField]
    PickupDataSO data;

    private void OnTriggerEnter(Collider collision)
    {
        PlayerManager pm = collision.GetComponent<PlayerManager>();
        if (pm != null)
        {
            PickUpObject(pm);
            DisplayUIPickup(collision);
        }
    }

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
