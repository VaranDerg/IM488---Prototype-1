using System;
using UnityEngine;

public interface IPoolableObject
{
    delegate void DeactivationHandler(object sender, PoolableObjectEventArgs e);
    event DeactivationHandler Deactivated;

    void Activate();

    void Deactivate();

    GameObject GetGameObject();

    void AssignPlayer(Player owner);

    Player GetPlayer();
}

public class PoolableObjectEventArgs : EventArgs
{
    public IPoolableObject Obj { get; private set; }

    public PoolableObjectEventArgs(IPoolableObject obj)
    {
        this.Obj = obj;
    }
}
