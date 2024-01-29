using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPool : MonoBehaviour
{
    [SerializeField]
    PoolSpawnType spawnType = PoolSpawnType.UNDER_SPAWNER;

    [SerializeField]
    float duration;

    [SerializeField]
    float startDelay;

    [SerializeField]
    float tickRate;

    [SerializeField]
    float tickRateScalar;

    [SerializeField]
    private TestSpellSO _thisSpell;

    float timeTillNextTick;

    protected Player owner { get; private set; }

    protected List<GameObject> objectsInPool = new();

    public abstract void Execute();

    protected abstract void ChildTick();

    protected abstract void OnSpawn();

    protected abstract void OnExpiration();

    public void Tick(float deltaTime)
    {
        Debug.Log("Ticked");
        if (timeTillNextTick > 0)
            timeTillNextTick -= deltaTime * tickRateScalar;
        else
        {
            Debug.Log("Execute");
            Execute();

            timeTillNextTick = tickRate;
        }

        ChildTick();
    }

    IEnumerator DelayedDeactivate()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }

    void Deactivate()
    {
        OnExpiration();

        ManagerParent.Instance.Particles.SpawnParticles(_thisSpell.SpellElement.BurstParticles, true, transform, false);

        gameObject.SetActive(false);
    }

    public void AssignPlayer(Player tag)
    {
        owner = tag;
    }

    private Vector3 GetSpawnPosition()
    {
        switch (spawnType)
        {
            case PoolSpawnType.UNDER_SPAWNER:
                return new Vector3(transform.position.x, 0, transform.position.z);

            case PoolSpawnType.RANDOM:
                Vector3 randomSphere = Random.insideUnitSphere;

                return new Vector3(randomSphere.x, 0, randomSphere.z).normalized;

            default:
                return Vector3.zero;
        }
    }

    protected bool DamageAllInside(float damage, bool doSelfDamage)
    {
        if (objectsInPool.Count == 0)
            return false;

        foreach (GameObject obj in objectsInPool)
        {
            PlayerManager player = obj.GetComponent<PlayerManager>();

            if (player == null)
                continue;

            bool isSelf = player.PlayerTag == owner;

            if (!isSelf || (doSelfDamage && isSelf))
                player.Damage(damage);
        }

        return true;
    }

    private void Start()
    {
        // Delays the first tick by the startDelay
        timeTillNextTick = startDelay;

        StartCoroutine(DelayedDeactivate());

        transform.position = GetSpawnPosition();

        OnSpawn();
    }

    private void FixedUpdate()
    {
        Tick(Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player") && other.GetComponent<PlayerManager>().PlayerTag == owner)
        {
            return;
        }

        objectsInPool.Add(other.gameObject);

        Debug.Log("Object added to " + gameObject.name + ": " + other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInPool.Remove(other.gameObject);
    }
}

public enum PoolSpawnType
{
    UNDER_SPAWNER,
    RANDOM
}