using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPickup : PickupAbstract, IPickup
{
    protected override void PickUpObject(PlayerManager pm)
    {
        pm.GetElementalStats().StartCoroutine(pm.GetElementalStats().
            TemporaryAddStat(GetScriptableObject().StatBuff, GetScriptableObject().PickupValue, GetScriptableObject().PickupDuration));

        PostPickup();
        Destroy(gameObject);    
    }
}
