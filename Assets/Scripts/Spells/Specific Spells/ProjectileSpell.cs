using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : AbstractSpell
{
    [SerializeField]
    GameObject projectilePrefab;

    ObjectPool pool;

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }

    public override void StartAura()
    {
        IPoolableObject obj = pool.GetObject();

        Projectile projectile = obj.GetGameObject().GetComponent<Projectile>();

        projectile.AssignPlayer(owner);

        projectile.Scale(MultiplayerManager.Instance.GetPlayer(owner).GetElementalStats());

        projectile.Activate();
    }

    protected override void AuraTick()
    {
        
    }
}
