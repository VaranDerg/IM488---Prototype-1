using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected Player owner { get; private set; }

    // The actual function of the spell to be referenced from the child
    public abstract void StartAura();

    // Called every FixedUpdate. Should be used if a spell needs to do something between executions
    protected abstract void AuraTick();

    protected virtual void OnStart()
    {

    }

    // Tracks the time remaining till next tick. Calls ChildTick each
    public void Tick(float deltaTime)
    {
        if (timeTillNextTick > 0)
            timeTillNextTick -= deltaTime * tickRateScalar;
        else
        {
            if(castMethod == CastingMethod.AUTO)
                StartAura();

            timeTillNextTick = tickRate;
        }

        AuraTick();
    }

    private void Start()
    {
        // Delays the first tick by the tickRate in seconds
        timeTillNextTick = tickRate;

        owner = transform.parent.parent.GetComponent<PlayerManager>().PlayerTag;
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
        tickRateScalar *= cooldownRateMult;
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
