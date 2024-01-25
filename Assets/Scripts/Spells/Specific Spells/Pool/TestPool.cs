using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPool : AbstractPool
{
    [SerializeField]
    ParticleSystem particles;

    public override void Execute()
    {
        

        Debug.Log("Objects in Pool: ");
        foreach (GameObject obj in objectsInPool)
            Debug.Log(obj.name);
    }

    protected override void ChildTick()
    {
        
    }

    protected override void OnExpiration()
    {
        
    }

    protected override void OnSpawn()
    {
        particles.Play();
    }

}
