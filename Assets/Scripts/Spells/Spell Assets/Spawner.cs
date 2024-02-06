using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    int tickAmt = 3;

    [SerializeField]
    float tickRate = 1;

    ObjectPool pool;

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }

    public void Spawn()
    {
        IPoolableObject obj = pool.GetObject();

        Projectile projectile = obj.GetGameObject().GetComponent<Projectile>();

        if(projectile != null)
        {
            projectile.Scale();

            projectile.transform.position = transform.position;

            projectile.Activate();

            return;
        }
        
        Pool spawnedPool = obj.GetGameObject().GetComponent<Pool>();
        if(spawnedPool != null)
        {
            spawnedPool.Scale();

            spawnedPool.transform.position = transform.position;

            spawnedPool.Activate();

            return;
        }

    }

}
