using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    int poolSize = 3;
    readonly Stack<IPoolableObject> inactiveObjects = new();

    private void Start()
    {
        for(int i = 0; i < poolSize; i++)
        {
            IPoolableObject obj = InstantiateNewObject();

            obj.GetGameObject().SetActive(false);

            inactiveObjects.Push(obj);
        }
    }

    public IPoolableObject GetObject()
    {
        if (!inactiveObjects.TryPop(out IPoolableObject returnObj))
            returnObj = InstantiateNewObject();

        return returnObj;
    }

    private IPoolableObject InstantiateNewObject()
    {
        GameObject obj = Instantiate(prefab);

        IPoolableObject poolable = obj.GetComponent<IPoolableObject>();
        if(poolable == null)
        {
            Debug.Log("Poolable null");
            Debug.Log(obj.name);
            return null;
        }

        poolable.Deactivated += OnDeactivation;

        return poolable;
    }

    private void OnDeactivation(object sender, PoolableObjectEventArgs e)
    {
        inactiveObjects.Push(e.Obj);
    }
}
