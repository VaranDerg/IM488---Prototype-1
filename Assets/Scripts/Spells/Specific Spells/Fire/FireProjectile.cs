using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : AbstractProjectile
{
    [SerializeField] private TestSpellSO _thisSpell;

    [SerializeField]
    float damage;

    protected override void OnEnvironmentCollision(Collision other)
    {
        //BounceOffSurface(other);
    }

    protected override void OnLaunch()
    {
        ManagerParent.Instance.Particles.SpawnParticles(_thisSpell.SpellElement.LoopingParticles, false, transform, true);
        ManagerParent.Instance.Audio.PlaySoundEffect(_thisSpell.SpellElement.SoundEffectName);
    }

    protected override void OnPlayerCollision(Collider other)
    {
        other.GetComponent<PlayerManager>().Damage(damage);
    }
}
