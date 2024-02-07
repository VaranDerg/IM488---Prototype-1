using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    int poolSize = 3;
    readonly Stack<IPoolableObject> inactiveObjects = new();

    public UnityEvent<GameObject> InstantiationEvent;

    Player owner;

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

        poolable.AssignPlayer(owner);

        poolable.Deactivated += OnDeactivation;

        return poolable;
    }

    private void OnDeactivation(object sender, PoolableObjectEventArgs e)
    {
        inactiveObjects.Push(e.Obj);
    }

    public void AssignPlayer()
    {
        owner = GetComponent<IPoolableObject>().GetPlayer();

        InstantiateObjects();
    }

    public void AssignPlayer(Player owner)
    {
        this.owner = owner;

        //Debug.Log("Object: " + gameObject.name + " | Owner Assigned: " + owner);
        InstantiateObjects();
    }

    private void InstantiateObjects()
    {
        for (int i = 0; i < poolSize; i++)
        {
            IPoolableObject obj = InstantiateNewObject();

            obj.AssignPlayer(owner);

            obj.GetGameObject().SetActive(false);

            inactiveObjects.Push(obj);

            InstantiationEvent.Invoke(obj.GetGameObject());
        }
    }
}
