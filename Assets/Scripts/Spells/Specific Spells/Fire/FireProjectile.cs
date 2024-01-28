using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : AbstractProjectile
{
    [SerializeField]
    float damage;

    protected override void OnEnvironmentCollision(Collision other)
    {
        //BounceOffSurface(other);
    }

    protected override void OnLaunch()
    {
        
    }

    protected override void OnPlayerCollision(Collider other)
    {
        other.GetComponent<PlayerManager>().Damage(damage);
    }
}
