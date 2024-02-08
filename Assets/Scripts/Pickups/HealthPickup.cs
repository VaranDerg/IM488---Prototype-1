using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupAbstract, IPickup
{
    protected override void PickUpObject(PlayerManager pm)
    {
        if(pm.GetPlayerHealth().Heal(GetScriptableObject().PickupValue))
        {
            Destroy(gameObject);
        }
        PostPickup();
    }
}
