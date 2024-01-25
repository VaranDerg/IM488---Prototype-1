using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPoolSpell : AbstractSpell
{
    [SerializeField]
    GameObject poolPrefab;

    public override void Execute()
    {
        GameObject projectileObj = Instantiate(poolPrefab, transform.position, Quaternion.identity);

        TestProjectile projectile = projectileObj.GetComponent<TestProjectile>();

        projectile.AssignPlayer(owner);

        projectile.Launch();
    }

    protected override void ChildTick()
    {
        
    }

}
