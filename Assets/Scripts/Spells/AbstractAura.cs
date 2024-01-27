using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAura : AbstractSpell
{
    [SerializeField]
    float auraTickRate;

    [SerializeField]
    float duration;

    [SerializeField]
    Renderer auraRenderer;

    float timeTillNextAuraTick;

    bool isAuraActive = false;

    protected List<GameObject> objectsInAura = new();

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

    protected override void ChildTick()
    {
        if (!isAuraActive)
            return;

        if (timeTillNextAuraTick > 0)
            timeTillNextAuraTick -= Time.fixedDeltaTime;
        else
        {
            ChildExecute();

            timeTillNextAuraTick = auraTickRate;
        }
    }

    public override void Execute()
    {
        StartCoroutine(DelayedDisable());
    }

    protected abstract void ChildExecute();

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
    }

    void DisableAura()
    {
        isAuraActive = false;

        if (auraRenderer != null)
            auraRenderer.enabled = false;
    }

    protected void DamageAllInAura(float damage)
    {
        if (objectsInAura.Count == 0)
            return;

        foreach (GameObject obj in objectsInAura)
        {
            PlayerManager player = obj.GetComponent<PlayerManager>();

            if (player == null)
                continue;

            bool isSelf = player.PlayerTag == owner;

            if (!isSelf)
                player.Damage(damage);
        }
    }
}
