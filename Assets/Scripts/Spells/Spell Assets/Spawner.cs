using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    int tickAmt = 3;

    [SerializeField]
    float tickRate = 1;

    [SerializeField]
    UnityEvent OnTickFinish;

    ObjectPool pool;

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }

    public void Spawn()
    {
        StartCoroutine(DelayedSpawn());

    }

    IEnumerator DelayedSpawn()
    {
        for(int i = 0; i < tickAmt; i++)
        {
            SpawnObj();

            yield return new WaitForSeconds(tickRate);
        }

        OnTickFinish.Invoke();
    }

    private void SpawnObj()
    {
        Debug.Log("Spawning Shard");

        IPoolableObject obj = pool.GetObject();

        Projectile projectile = obj.GetGameObject().GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Scale();

            projectile.transform.position = transform.position;

            projectile.Activate();

            return;
        }

        Pool spawnedPool = obj.GetGameObject().GetComponent<Pool>();
        if (spawnedPool != null)
        {
            spawnedPool.Scale();

            spawnedPool.transform.position = transform.position;

            spawnedPool.Activate();

            return;
        }
    }

}
