using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupAbstract, IPickup
{
    [SerializeField] float _healAmount;
    protected override void PickUpObject(PlayerManager pm)
    {
        if(pm.GetPlayerHealth().Heal(_healAmount))
        {
            Destroy(gameObject);
        }
        PostPickup();
    }
}
