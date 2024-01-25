using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPoolSpell : AbstractSpell
{
    [SerializeField]
    GameObject poolPrefab;

    public override void Execute()
    {
        GameObject poolObj = Instantiate(poolPrefab, transform.position, Quaternion.identity);

        TestPool pool = poolObj.GetComponent<TestPool>();

        pool.AssignPlayer(owner);
    }

    protected override void ChildTick()
    {
        
    }

}
