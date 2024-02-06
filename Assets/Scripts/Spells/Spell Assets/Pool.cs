using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pool : MonoBehaviour, IScalable, IPoolableObject
{
    [SerializeField]
    PoolSpawnType spawnType = PoolSpawnType.UNDER_SPAWNER;

    [SerializeField]
    float duration;

    [SerializeField]
    float startDelay = 1;

    [SerializeField]
    float tickRate = 1;

    [SerializeField]
    float tickRateScalar = 1;

    [SerializeField]
    float damage = 1;

    [SerializeField]
    bool doSelfDamage = false;

    [SerializeField]
    bool isPersistent = true;

    [SerializeField]
    private TestSpellSO _thisSpell;

    [SerializeField]
    UnityEvent OnTriggeredEvent;

    float timeTillNextTick;

    protected Player owner { get; private set; }

    protected List<GameObject> objectsInPool = new();

    public event IPoolableObject.DeactivationHandler Deactivated;

    Vector3 startSize;

    Vector3 scaledPoolSize = Vector3.one;
    float scaledPoolDamage = 1;

    private void Awake()
    {
        startSize = transform.localScale;
    }

    public void Execute()
    {
        if (DamageAllInside(scaledPoolDamage, doSelfDamage) && !isPersistent)
        {
            OnTriggeredEvent.Invoke();
            gameObject.SetActive(false);
        }
            
    }

    public void Scale(ElementalStats stats)
    {
        ScaleSize(stats.GetStat(ScalableStat.POOL_SIZE));
        ScaleDamage(stats.GetStat(ScalableStat.DAMAGE));
    }

    public void Scale()
    {
        Scale(MultiplayerManager.Instance.GetPlayer(owner).GetElementalStats());
    }

    private void ScaleSize(float sizeMult)
    {
        scaledPoolSize = startSize * sizeMult;
        transform.localScale = scaledPoolSize;
    }

    private void ScaleDamage(float damageMult)
    {
        scaledPoolDamage = damage * damageMult;
    }

    protected void ChildTick()
    {

    }

    protected void OnSpawn()
    {

    }

    protected void OnExpiration()
    {

    }

    public void Tick(float deltaTime)
    {
        //Debug.Log("Ticked");
        if (timeTillNextTick > 0)
            timeTillNextTick -= deltaTime * tickRateScalar;
        else
        {
            //Debug.Log("Execute");
            Execute();

            timeTillNextTick = tickRate;
        }

        ChildTick();
    }

    
    public void Activate()
    {
        gameObject.SetActive(true);

        Scale();

        timeTillNextTick = startDelay;

        StartCoroutine(DelayedDeactivate());

        transform.position = GetSpawnPosition();

        OnSpawn();
    }

    public void Deactivate()
    {
        OnExpiration();

        objectsInPool.Clear();

        Deactivated.Invoke(this, new PoolableObjectEventArgs(this));

        ManagerParent.Instance.Particles.SpawnParticles(_thisSpell.SpellElement.BurstParticles, true, transform, false);

        gameObject.SetActive(false);
    }

    IEnumerator DelayedDeactivate()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }

    public void AssignPlayer(Player tag)
    {
        owner = tag;
    }

    public Player GetPlayer()
    {
        return owner;
    }

    private Vector3 GetSpawnPosition()
    {
        switch (spawnType)
        {
            case PoolSpawnType.UNDER_SPAWNER:
                return new Vector3(transform.position.x, 0.4f, transform.position.z);

            case PoolSpawnType.RANDOM:
                Vector3 randomSphere = Random.insideUnitSphere;

                return new Vector3(randomSphere.x, 0.4f, randomSphere.z).normalized;

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
            if (obj == null)
                continue;

            PlayerManager player = obj.GetComponent<PlayerManager>();

            if (player == null)
                continue;

            bool isSelf = player.PlayerTag == owner;

            if (!isSelf || (doSelfDamage && isSelf))
                player.Damage(damage, InvulnTypes.DASHINVULN);
        }

        return true;
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

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

public enum PoolSpawnType
{
    UNDER_SPAWNER,
    RANDOM
}