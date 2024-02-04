using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    int poolSize = 3;

    Stack<IPoolableObject> inactiveObjects = new();

    private void Start()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);

            IPoolableObject poolable = obj.GetComponent<IPoolableObject>();
            poolable.Deactivated += OnDeactivation;

            inactiveObjects.Push(poolable);
        }
    }

    public IPoolableObject GetObject()
    {
        return inactiveObjects.Pop();
    }

    private void OnDeactivation(object sender, PoolableObjectEventArgs e)
    {
        inactiveObjects.Push(e.Obj);
    }
}
