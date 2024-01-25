using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : AbstractProjectile
{
    protected override void OnEnvironmentCollision(Collision other)
    {
        Debug.Log("Environment Collision");
        BounceOffSurface(other);
    }

    protected override void OnLaunch()
    {
        Debug.Log("Launch");
    }

    protected override void OnPlayerCollision(Collider other)
    {
        Debug.Log("Player Collision");
    }


}
