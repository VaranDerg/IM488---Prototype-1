using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPool : AbstractPool
{
    [SerializeField]
    ParticleSystem particles;

    public override void Execute()
    {
        particles.Play();
    }

    protected override void ChildTick()
    {
        
    }

}
