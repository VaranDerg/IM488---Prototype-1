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

    [SerializeField] float randomSpeedVariance;

    [SerializeField] bool canBounce;
    [SerializeField] float projectileLifeTime;

    Rigidbody rb;
    private Vector3 lastVelocity;

    protected Player owner { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(TargetReeval());
        StartCoroutine(TrackLastVelocity());
        StartCoroutine(LifeTime());
    }

    #region Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerManager>().PlayerTag == owner)
                return;

            OnPlayerCollision(other);
        }
        

        if (!isPersistent)
            Deactivate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("HitWall");
        if (collision.gameObject.CompareTag("Environment"))
            OnEnvironmentCollision(collision);
    }
    #endregion

    private void FixedUpdate()
    {
        /*if (doReevaluateTargetting)
            Move();*/
    }

    private IEnumerator TargetReeval()
    {
        while(doReevaluateTargetting)
        {
            Move();
            yield return new WaitForFixedUpdate();
        }
    }

    public void AssignPlayer(Player tag)
    {
        owner = tag;
    }

    public void Launch()
    {
        SpeedVariance();

        Move();

        OnLaunch();
    }

    private void SpeedVariance()
    {
        projectileSpeed = Random.Range(projectileSpeed - randomSpeedVariance, projectileSpeed + randomSpeedVariance);
    }

    protected void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(projectileLifeTime);
        Destroy(gameObject);
    }

    private IEnumerator TrackLastVelocity()
    {
        while(canBounce)
        {
            yield return new WaitForEndOfFrame();
            lastVelocity = rb.velocity;
        }
    }

    public void BounceOffSurface(Collision other)
    {
        Vector3 bounceDirection = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);
        rb.velocity = bounceDirection * lastVelocity.magnitude;
    }

    protected virtual Vector3 GetTargetDirection()
    {
        switch(targetType){
            case TargetType.OTHER_PLAYER:
                Vector3 enemyTargetDir = (MultiplayerManager.Instance.GetOpposingPlayer(owner).transform.position
                    - MultiplayerManager.Instance.GetPlayer(owner).transform.position).normalized;
                return new Vector3(enemyTargetDir.x, 0, enemyTargetDir.z);

            case TargetType.MOVE_DIRECTION:
                PlayerManager player = MultiplayerManager.Instance.GetPlayer(owner);
                return player.GetLastNonZeroMovement().normalized;
            //return player.GetMovementDirection();
            case TargetType.OPPOSITE_DIRECTION:
                player = MultiplayerManager.Instance.GetPlayer(owner);
                return player.GetLastNonZeroMovement().normalized * -1;

            case TargetType.RANDOM:
                /*return new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)).normalized 
                    + MultiplayerManager.Instance.GetPlayer(owner).transform.position;*/
                Vector3 randomSphere = Random.insideUnitSphere;
                //Debug.Log(new Vector3(randomSphere.x, 0, randomSphere.z).normalized);
                return new Vector3(randomSphere.x, 0, randomSphere.z).normalized;

            default:
                return Vector3.zero;
        }
    }

    protected virtual void Move()
    {
        rb.AddForce(GetTargetDirection() * projectileSpeed, ForceMode.Impulse);
    }

    // Child Functions
    protected abstract void OnPlayerCollision(Collider other);

    protected abstract void OnEnvironmentCollision(Collision other);

    protected abstract void OnLaunch();
}

public enum TargetType
{
    OTHER_PLAYER,
    MOVE_DIRECTION,
    OPPOSITE_DIRECTION,
    RANDOM,
    OTHER
}