using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceObject : AbstractPool
{
    [SerializeField]
    float damage;

    public override void Execute()
    {
        DamageAllInside(damage, false);
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
