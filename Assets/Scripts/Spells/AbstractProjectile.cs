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

    Rigidbody rb;

    protected Player owner { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerManager>().PlayerTag == owner)
                return;

            OnPlayerCollision(other);
        }
        else
            OnEnvironmentCollision(other);

        if (!isPersistent)
            Deactivate();
    }

    private void FixedUpdate()
    {
        if (doReevaluateTargetting)
            Move();
    }

    public void AssignPlayer(Player tag)
    {
        owner = tag;
    }

    public void Launch()
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
                return MultiplayerManager.Instance.GetPlayer(owner).transform.position - 
                    MultiplayerManager.Instance.GetOpposingPlayer(owner).transform.position;

            case TargetType.MOVE_DIRECTION:
                PlayerManager player = MultiplayerManager.Instance.GetPlayer(owner);

                return player.GetMovementDirection();

            case TargetType.RANDOM:
                return new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

            default:
                return Vector3.zero;
        }
    }

    protected virtual void Move()
    {
        rb.AddForce(GetTargetLocation() * projectileSpeed, ForceMode.Impulse);
    }

    // Child Functions
    protected abstract void OnPlayerCollision(Collider other);

    protected abstract void OnEnvironmentCollision(Collider other);

    protected abstract void OnLaunch();
}

public enum TargetType
{
    OTHER_PLAYER,
    MOVE_DIRECTION,
    RANDOM,
    OTHER
}