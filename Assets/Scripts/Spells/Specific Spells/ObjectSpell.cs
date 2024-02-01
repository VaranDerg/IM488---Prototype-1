using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpell : AbstractSpell
{
    [SerializeField]
    GameObject objPrefab;

    public override void StartAura()
    {
        GameObject poolObj = Instantiate(objPrefab, transform.position, Quaternion.identity);

        Pool pool = poolObj.GetComponent<Pool>();

        pool.AssignPlayer(owner);

        if (GetScriptableObject() == null)
        {
            Debug.Log("SpellSO was null");
            return;
        }

        ManagerParent.Instance.Particles.SpawnParticles(GetScriptableObject().SpellElement.LoopingParticles, false, pool.transform, true);
        ManagerParent.Instance.Audio.PlaySoundEffect(GetScriptableObject().SpellElement.SoundEffectName);
    }

    protected override void AuraTick()
    {
        
    }
}
