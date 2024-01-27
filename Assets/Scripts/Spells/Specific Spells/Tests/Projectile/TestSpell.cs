using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : AbstractSpell
{
    [SerializeField]
    ParticleSystem particles;

    [SerializeField]
    GameObject particlePrefab;

    public override void Execute()
    {
        particles.Play();

        GameObject projectileObj = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        TestProjectile projectile = projectileObj.GetComponent<TestProjectile>();

        projectile.AssignPlayer(owner);

        projectile.Launch();
    }

    protected override void ChildTick()
    {
        
    }
}
