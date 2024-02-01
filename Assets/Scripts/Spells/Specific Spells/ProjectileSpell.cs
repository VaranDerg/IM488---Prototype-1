using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : AbstractSpell
{
    [SerializeField]
    GameObject projectilePrefab;

    public override void StartAura()
    {
        GameObject projectileObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Projectile projectile = projectileObj.GetComponent<Projectile>();

        projectile.AssignPlayer(owner);

        projectile.Launch();
    }

    protected override void AuraTick()
    {
        
    }
}
