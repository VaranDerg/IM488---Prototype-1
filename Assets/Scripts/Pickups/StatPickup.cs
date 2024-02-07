using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPickup : PickupAbstract, IPickup
{
    protected override void PickUpObject(PlayerManager pm)
    {
        foreach(ScalableStat statBuff in GetScriptableObject().StatBuff)
        {
            pm.GetElementalStats().StartCoroutine(pm.GetElementalStats().
                TemporaryAddStat(statBuff, GetScriptableObject().PickupValue, GetScriptableObject().PickupDuration));
        }
        

        PostPickup();
        Destroy(gameObject);    
    }
}
