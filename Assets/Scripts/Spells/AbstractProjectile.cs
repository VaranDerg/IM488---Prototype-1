using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    TargetType targetType = TargetType.RANDOM;

    [SerializeField]
    [Tooltip("If true, reevaluates its target's position every FixedUpdate. For homing effects.")]
    bool doReevaluateTargetting = false;

    [SerializeField]
    [Tooltip("If true, doesn't get automatically removed upon collision. For piercing / bouncing effects.")]
    bool isPersistent = false;

    [SerializeField]
    float projectileSpeed = 1;

    Collider hitbox;

    protected PlayerTag owner { get; private set; }
    private void Awake()
    {
        hitbox = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerManager>().PlayerTag != owner)
        {
            OnPlayerCollision();
        }
        else
            OnEnvironmentCollision();

        if(!isPersistent)
            Deactivate();
    }

    private void FixedUpdate()
    {
        if (doReevaluateTargetting)
            Move();
    }

    public void AssignPlayer(PlayerTag tag)
    {
        owner = tag;
    }

    protected void Launch()
    {
        Move();

        OnLaunch();
    }

    protected void Deactivate()
    {
        gameObject.SetActive(false);
    }

    protected virtual Vector3 GetTargetLocation()
    {
        switch(targetType){
            case TargetType.OTHER_PLAYER:
                return Vector3.zero;

            case TargetType.MOVE_DIRECTION:
                return Vector3.zero;

            case TargetType.RANDOM:
                return Vector3.zero;

            default:
                return Vector3.zero;
        }
    }

    protected virtual void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetTargetLocation(), projectileSpeed);
    }

    // Child Functions
    protected abstract void OnPlayerCollision();

    protected abstract void OnEnvironmentCollision();

    protected abstract void OnLaunch();
}

public enum TargetType
{
    OTHER_PLAYER,
    MOVE_DIRECTION,
    RANDOM,
    OTHER
}