using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileSpell : AbstractSpell
{

    [SerializeField]
    UnityEvent OnStartAuraEvent;

    ObjectPool pool;

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }

    public override void StartAura()
    {
        base.StartAura();

        SpawnProjectile();

        OnStartAuraEvent.Invoke();
    }

    public void SpawnProjectile()
    {
        IPoolableObject obj = pool.GetObject();

        Projectile projectile = obj.GetGameObject().GetComponent<Projectile>();

        projectile.AssignPlayer(owner);

        projectile.Scale(MultiplayerManager.Instance.GetPlayer(owner).GetElementalStats());

        projectile.transform.position = transform.position;

        projectile.Activate();
    }

    public void DelayedSpawnProjectile()
    {
        StartCoroutine(DelayedSpawn());
    }

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        SpawnProjectile();
    }

    protected override void AuraTick()
    {
        
    }

    public void LightningAura()
    {
        StartCoroutine(DelayedLightning());
    }

    IEnumerator DelayedLightning()
    {
        transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.15f);

        for (int i = 0; i < 3; i++)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(0.1f);
        }

        transform.GetChild(0).gameObject.SetActive(false);
    }
}
