using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPool : MonoBehaviour
{
    [SerializeField]
    float duration;

    [SerializeField]
    float startDelay;

    [SerializeField]
    float tickRate;

    [SerializeField]
    float tickRateScalar;

    float timeTillNextTick;

    protected Player owner { get; private set; }

    protected List<GameObject> objectsInPool = new();

    public abstract void Execute();

    protected abstract void ChildTick();

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

    IEnumerator DelayedDeactivate()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        // Delays the first tick by the startDelay
        timeTillNextTick = startDelay;

        StartCoroutine(DelayedDeactivate());
    }

    private void FixedUpdate()
    {
        Tick(Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        objectsInPool.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInPool.Remove(other.gameObject);
    }
}
