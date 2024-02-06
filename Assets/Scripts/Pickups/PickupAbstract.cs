using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupAbstract : MonoBehaviour, IPickup
{
    protected abstract void PickUpObject(PlayerManager pm);

    private void OnTriggerEnter(Collider collision)
    {
        PlayerManager pm = collision.GetComponent<PlayerManager>();
        if (pm != null)
        {
            PickUpObject(pm);
        }
    }

    public void PostPickup()
    {
        PickupManager.Instance.RemovePickupFromLocation(gameObject);
        Destroy(gameObject);
    }
}
