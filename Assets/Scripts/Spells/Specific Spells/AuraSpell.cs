using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraSpell : AbstractSpell
{
    [SerializeField]
    float auraTickRate;

    [SerializeField]
    float duration;

    [SerializeField]
    float damage;

    [SerializeField]
    Renderer auraRenderer;

    float timeTillNextAuraTick;

    bool isAuraActive = false;

    protected List<GameObject> objectsInAura = new();

    private GameObject _currentParticles;

    private void OnTriggerEnter(Collider other)
    {
        objectsInAura.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInAura.Remove(other.gameObject);
    }

    protected override void OnStart()
    {
        timeTillNextAuraTick = auraTickRate;
        DisableAura();
    }

    protected override void AuraTick()
    {
        if (!isAuraActive)
            return;

        if (timeTillNextAuraTick > 0)
            timeTillNextAuraTick -= Time.fixedDeltaTime;
        else
        {
            OnAuraTick();

            timeTillNextAuraTick = auraTickRate;
        }
    }

    public override void StartAura()
    {
        StartCoroutine(DelayedDisable());
    }

    protected void OnAuraTick()
    {
        DamageAllInAura(damage);
    }

    IEnumerator DelayedDisable()
    {
        EnableAura();
        yield return new WaitForSeconds(duration);
        DisableAura();
    }

    void EnableAura()
    {
        isAuraActive = true;

        if (auraRenderer != null)
            auraRenderer.enabled = true;

        DisableParticles();

        _currentParticles = ManagerParent.Instance.Particles.SpawnParticles(GetScriptableObject().SpellElement.LoopingParticles, false, transform, true);
        ManagerParent.Instance.Audio.PlaySoundEffect(GetScriptableObject().SpellElement.SoundEffectName);
    }

    private void DisableParticles()
    {
        if (_currentParticles != null)
        {
            Destroy(_currentParticles);
            _currentParticles = null;
        }
    }

    void DisableAura()
    {
        isAuraActive = false;

        if (auraRenderer != null)
            auraRenderer.enabled = false;

        ManagerParent.Instance.Particles.SpawnParticles(GetScriptableObject().SpellElement.BurstParticles, true, transform, false);

        DisableParticles();
    }

    protected void DamageAllInAura(float damage)
    {
        if (objectsInAura.Count == 0)
            return;

        foreach (GameObject obj in objectsInAura)
        {
            if (obj == null)
                continue;

            PlayerManager player = obj.GetComponent<PlayerManager>();

            if (player == null)
                continue;

            bool isSelf = player.PlayerTag == owner;

            if (!isSelf)
                player.Damage(damage, InvulnTypes.DASHINVULN);
        }
    }
}
