using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpell : AbstractSpell
{
    [SerializeField]
    GameObject objPrefab;

    [SerializeField] private TestSpellSO _thisSpell;

    public override void Execute()
    {
        GameObject poolObj = Instantiate(objPrefab, transform.position, Quaternion.identity);

        AbstractPool pool = poolObj.GetComponent<AbstractPool>();

        pool.AssignPlayer(owner);

        ManagerParent.Instance.Particles.SpawnParticles(_thisSpell.SpellElement.LoopingParticles, false, transform, true);
        ManagerParent.Instance.Audio.PlaySoundEffect(_thisSpell.SpellElement.SoundEffectName);
    }

    protected override void ChildTick()
    {
        
    }
}
