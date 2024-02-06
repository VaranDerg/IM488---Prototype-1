using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour, IScalable, ICanUsePortal, IPoolableObject
{
    [SerializeField]
    TestSpellSO _thisSpell;

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

    [SerializeField]
    float damage;

    [SerializeField] float randomSpeedVariance;

    [SerializeField] bool rotateTowardsTarget = false;
    [SerializeField] bool canBounce;
    [SerializeField] bool isStatic = false;
    [SerializeField] float projectileLifeTime;
    [SerializeField] LayerMask bounceLayerMask;
    private const float _bounceRaycastDist = 2;

    Rigidbody rb;
    private Vector3 lastVelocity;

    public event IPoolableObject.DeactivationHandler Deactivated;

    bool hasBeenScaled = false;

    [SerializeField]
    UnityEvent OnAssignPlayer;

    [SerializeField]
    UnityEvent OnLaunchEvent;

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
        //Debug.Log("Owner: " + owner);
        //Debug.Log("Hit: " + other.name);

        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerManager>().PlayerTag == owner)
                return;
            //Debug.Log("Hit Player! Tag: " + other.GetComponent<PlayerManager>().PlayerTag);
            OnPlayerCollision(other);
        }
        
        if(other.gameObject.CompareTag("Environment"))
        {
            OnEnvironmentCollision(other);
        }

        if (!isPersistent)
            Deactivate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("HitWall");
        /*if (collision.gameObject.CompareTag("Environment"))
            OnEnvironmentCollision(collision);*/
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

        OnAssignPlayer.Invoke();
    }

    public Player GetPlayer()
    {
        return owner;
    }

    private void Launch()
    {
        SpeedVariance();

        Move();

        OnLaunch();

        OnLaunchEvent.Invoke();
    }

    // For elemental stats
    public void Scale(ElementalStats stats)
    {
        if (hasBeenScaled)
            return;

        hasBeenScaled = true;
        ScaleSpeed(stats.GetStat(ScalableStat.PROJECTILE_SPEED));
        ScaleSize(stats.GetStat(ScalableStat.PROJECTILE_SIZE));
        ScaleDamage(stats.GetStat(ScalableStat.DAMAGE));
    }

    public void Scale()
    {
        Scale(MultiplayerManager.Instance.GetPlayer(owner).GetElementalStats());
    }

    private void ScaleSpeed(float speedMult)
    {
        projectileSpeed *= speedMult;
    }

    private void ScaleSize(float sizeMult)
    {
        transform.localScale *= sizeMult;
    }

    private void ScaleDamage(float damageMult)
    {
        damage *= damageMult;
    }

    private void SpeedVariance()
    {
        projectileSpeed = Random.Range(projectileSpeed - randomSpeedVariance, projectileSpeed + randomSpeedVariance);
    }

    public void Activate()
    {
        gameObject.SetActive(true);

        Launch();
    }

    public void Deactivate()
    {
        Deactivated.Invoke(this, new PoolableObjectEventArgs(this));

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

    public void TeleportTo(Vector3 newLoc)
    {
        transform.position = newLoc;
    }

    public void BounceOffSurface(Collider wall)
    {
        Vector3 closestPoint = Physics.ClosestPoint(transform.position, wall.gameObject.GetComponent<Collider>(),wall.transform.position,wall.transform.rotation);

        Vector3 dir = (closestPoint-transform.position).normalized;
        dir = new Vector3(dir.x, 0, dir.z);


        RaycastHit rayHit;
        if(Physics.Raycast(transform.position, dir, out rayHit, _bounceRaycastDist, bounceLayerMask))
        {
            Vector3 bounceDirection = Vector3.Reflect(lastVelocity.normalized, rayHit.normal);
            rb.velocity = bounceDirection * lastVelocity.magnitude;
        }
        //other.contacts[0].normal
        
        
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
            case TargetType.RANDOM_IN_RANGE:
                randomSphere = Random.insideUnitSphere;
                //Debug.Log(new Vector3(randomSphere.x, 0, randomSphere.z).normalized);
                return (new Vector3(randomSphere.x, 0, randomSphere.z).normalized) * projectileSpeed;

            default:
                return Vector3.zero;
        }
    }

    protected virtual void Move()
    {
        if (isStatic)
        {
            transform.position += GetTargetDirection();
            return;
        }

        Vector3 forceVector = GetTargetDirection() * projectileSpeed;
        rb.AddForce(forceVector, ForceMode.Impulse);
        transform.rotation = Quaternion.LookRotation(forceVector);
    }

    // Child Functions
    protected virtual void OnPlayerCollision(Collider other)
    {
        other.GetComponent<PlayerManager>().Damage(damage, InvulnTypes.FULLINVULN);
    }

    protected virtual void OnEnvironmentCollision(Collider other)
    {
        if(canBounce)
        {
            BounceOffSurface(other);
        }
    }

    protected virtual void OnLaunch()
    {
        if (_thisSpell == null)
        {
            Debug.Log("SpellSO was null");
            return;
        }

        ManagerParent.Instance.Particles.SpawnParticles(_thisSpell.SpellElement.LoopingParticles, false, transform, true);
        ManagerParent.Instance.Audio.PlaySoundEffect(_thisSpell.SpellElement.SoundEffectName);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

public enum TargetType
{
    OTHER_PLAYER,
    MOVE_DIRECTION,
    OPPOSITE_DIRECTION,
    RANDOM,
    RANDOM_IN_RANGE,
    OTHER
}