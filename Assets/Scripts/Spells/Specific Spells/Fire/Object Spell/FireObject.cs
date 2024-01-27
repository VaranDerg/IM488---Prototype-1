using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : AbstractPool
{
    [SerializeField]
    float damage;

    [SerializeField]
    bool selfDamage = true;

    public override void Execute()
    {
        DamageAllInside(damage, selfDamage);

        gameObject.SetActive(false);
    }

    protected override void ChildTick()
    {
        
    }

    protected override void OnExpiration()
    {
        
    }

    protected override void OnSpawn()
    {
        
    }
}
