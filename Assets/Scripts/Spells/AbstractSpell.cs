using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractSpell : MonoBehaviour, ISpell, IScalable
{
    
    float ISpell.TickRate { get => tickRate; set => this.tickRate = value; }
    float ISpell.TickRateScalar { get => tickRateScalar; set => this.tickRateScalar = value; }

    [SerializeField]
    TestSpellSO data;

    // Essentially the 'cooldown' of a spell. Time between its executions in seconds
    [SerializeField]
    float tickRate = 1;

    // Scalar that can modify how frequently a spell executes
    [SerializeField]
    float tickRateScalar = 1;

    float timeTillNextTick = 1;

    [SerializeField] CastingMethod castMethod;

    [SerializeField] float castDelay = 0;

    float scaledTickRateScalar = 1;
    public Player owner { get; set; }

    // The actual function of the spell to be referenced from the child
    public abstract void StartAura();

    // Called every FixedUpdate. Should be used if a spell needs to do something between executions
    protected abstract void AuraTick();

    protected virtual void OnStart()
    {

    }

    public void DelayedStartAura()
    {
        StartCoroutine(DelayedAura());
    }

    IEnumerator DelayedAura()
    {
        yield return new WaitForSeconds(castDelay);
        StartAura();
    }

    // Tracks the time remaining till next tick. Calls ChildTick each
    public void Tick(float deltaTime)
    {
        if (timeTillNextTick > 0)
            timeTillNextTick -= deltaTime * scaledTickRateScalar;
        else
        {
            if (castMethod == CastingMethod.AUTO)
                DelayedStartAura();

            timeTillNextTick = tickRate;
        }

        AuraTick();
    }

    private void Start()
    {
        // Delays the first tick by the tickRate in seconds
        timeTillNextTick = tickRate;
        scaledTickRateScalar = tickRate;

        owner = transform.parent.parent.GetComponent<PlayerManager>().PlayerTag;

        ObjectPool objectPool = GetComponent<ObjectPool>();
        if(objectPool != null)
            objectPool.AssignPlayer(owner);
        //Debug.Log(owner);
        AddSpellsToLists();

        StartCoroutine(DelayedStart());

        OnStart();
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Scale(MultiplayerManager.Instance.GetPlayer(owner).GetElementalStats());
    }

    protected void AddSpellsToLists()
    {
        switch (castMethod)
        {
            case CastingMethod.AUTO:
                return;
            case CastingMethod.DASH:
                MultiplayerManager.Instance.GetPlayer(owner).GetComponent<Controller>().AddDashSpellToList(this);
                return;
        }

    }

    public void Scale(ElementalStats stats)
    {
        ScaleCooldownRate(stats.GetStat(ScalableStat.COOLDOWN_RATE));

        ChildScale(stats);
    }

    private void ScaleCooldownRate(float cooldownRateMult)
    {
        scaledTickRateScalar = tickRateScalar * cooldownRateMult;
    }

    protected virtual void ChildScale(ElementalStats stats)
    {

    }

    public TestSpellSO GetScriptableObject()
    {
        return data;
    }

    private void FixedUpdate()
    {
        Tick(Time.fixedDeltaTime);
    }
}

public enum CastingMethod
{
    AUTO,
    DASH
}
