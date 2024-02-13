using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpell : AbstractSpell
{
    [SerializeField]
    bool soundEnabled = true;

    ObjectPool pool;

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }

    public override void StartAura()
    {
        base.StartAura();

        GameObject poolObj = pool.GetObject().GetGameObject();

        Pool puddle = poolObj.GetComponent<Pool>();

        puddle.AssignPlayer(owner);

        puddle.Scale(MultiplayerManager.Instance.GetPlayer(owner).GetElementalStats());

        puddle.transform.position = transform.position;

        puddle.Activate();

        if (GetScriptableObject() == null)
        {
            Debug.Log("SpellSO was null");
            return;
        }

        //ManagerParent.Instance.Particles.SpawnParticles(GetScriptableObject().SpellElement.LoopingParticles, false, puddle.transform, true);
        if(soundEnabled)
            ManagerParent.Instance.Audio.PlaySoundEffect(GetScriptableObject().SpellElement.SoundEffectName);
    }

    protected override void AuraTick()
    {
        
    }
}
