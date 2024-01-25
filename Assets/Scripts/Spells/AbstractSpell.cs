using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSpell : MonoBehaviour, ISpell
{
    
    float ISpell.TickRate { get => tickRate; set => this.tickRate = value; }
    float ISpell.TickRateScalar { get => tickRateScalar; set => this.tickRateScalar = value; }

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
    public abstract void Execute();

    // Called every FixedUpdate. Should be used if a spell needs to do something between executions
    protected abstract void ChildTick();

    // Tracks the time remaining till next tick. Calls ChildTick each
    public void Tick(float deltaTime)
    {
        if (timeTillNextTick > 0)
            timeTillNextTick -= deltaTime * tickRateScalar;
        else
        {
            Execute();

            timeTillNextTick = tickRate;
        }

        ChildTick();
    }

    private void Start()
    {
        // Delays the first tick by the tickRate in seconds
        timeTillNextTick = tickRate;

        owner = transform.parent.parent.GetComponent<PlayerManager>().PlayerTag;
        Debug.Log(owner);
        AddSpellsToLists();
    }

    protected void AddSpellsToLists()
    {
        switch (castMethod)
        {
            case CastingMethod.AUTO:
                return;
            case CastingMethod.MANUAL:
                MultiplayerManager.Instance.GetPlayer(owner).GetComponent<Controller>().AddManualSpellToList(this);
                return;
            case CastingMethod.DASH:
                MultiplayerManager.Instance.GetPlayer(owner).GetComponent<Controller>().AddDashSpellToList(this);
                return;
        }

    }

    private void FixedUpdate()
    {
        Tick(Time.fixedDeltaTime);
    }
}

public enum CastingMethod
{
    AUTO,
    MANUAL,
    DASH
}
