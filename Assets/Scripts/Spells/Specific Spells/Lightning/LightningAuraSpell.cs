using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAuraSpell : AbstractAura
{
    [SerializeField]
    float damage;

    protected override void ChildExecute()
    {
        DamageAllInAura(damage);
    }
}
