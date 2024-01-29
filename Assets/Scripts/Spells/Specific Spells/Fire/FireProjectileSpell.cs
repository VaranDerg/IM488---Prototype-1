using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileSpell : AbstractSpell
{
    [SerializeField]
    GameObject particlePrefab;

    public override void Execute()
    {
        GameObject projectileObj = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        FireProjectile projectile = projectileObj.GetComponent<FireProjectile>();

        projectile.AssignPlayer(owner);

        projectile.Launch();
    }

    protected override void ChildTick()
    {
        
    }
}
