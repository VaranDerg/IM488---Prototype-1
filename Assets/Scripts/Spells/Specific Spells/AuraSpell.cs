using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AuraSpell : AbstractSpell
{
    [SerializeField]
    float auraTickRate;

    [SerializeField]
    float duration;

    [SerializeField]
    float damage;

    [Tooltip("Only used for ring-shaped colliders. Only triggers if the colliding body is at least this distance away from the spell's center.")]
    [SerializeField]
    float minDistance = 0;

    [SerializeField]
    bool scaleMinDistanceWithSize = true;

    [SerializeField]
    Renderer auraRenderer;

    [SerializeField]
    UnityEvent OnAuraStart;

    float timeTillNextAuraTick;

    bool isAuraActive = false;

    protected List<GameObject> objectsInAura = new();

    private GameObject _currentParticles;
    private void OnTriggerEnter(Collider other)
    {
        if (HorizontalDistance(other.transform.position) < minDistance)
            return;

        objectsInAura.Add(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (objectsInAura.Contains(other.gameObject))
            return;

        if (HorizontalDistance(other.transform.position) < minDistance)
            return;

        objectsInAura.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInAura.Remove(other.gameObject);
    }

    protected override void OnStart()
    {
        timeTillNextAuraTick = auraTickRate;

        if (scaleMinDistanceWithSize)
            minDistance *= transform.localScale.x;

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
        OnAuraStart.Invoke();
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

            if (HorizontalDistance(obj.transform.position) < minDistance)
                continue;

            PlayerManager player = obj.GetComponent<PlayerManager>();

            if (player == null)
                continue;

            bool isSelf = player.PlayerTag == owner;

            if (!isSelf)
                player.Damage(damage, InvulnTypes.DASHINVULN);
        }
    }

    protected override void ChildScale(ElementalStats stats)
    {
        ScaleSize(stats.GetStat(ScalableStat.AURA_SIZE));
        ScaleDamage(stats.GetStat(ScalableStat.DAMAGE));
    }

    private void ScaleSize(float sizeMult)
    {
        transform.localScale *= sizeMult;
    }

    private void ScaleDamage(float damageMult)
    {
        damage *= damageMult;
    }

    private float HorizontalDistance(Vector3 otherObjPosition)
    {
        Vector3 thisPos = transform.position;

        thisPos.y = 0;
        otherObjPosition.y = 0;

        return (otherObjPosition - thisPos).magnitude;
    }
}
