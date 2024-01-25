using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : AbstractProjectile
{
    protected override void OnEnvironmentCollision(Collider other)
    {
        Debug.Log("Environment Collision");
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
