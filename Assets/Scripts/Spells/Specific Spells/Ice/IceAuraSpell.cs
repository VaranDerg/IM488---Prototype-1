using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAuraSpell : AbstractAura
{
    [SerializeField]
    float damage;

    protected override void ChildExecute()
    {
        DamageAllInAura(damage);
    }
}
