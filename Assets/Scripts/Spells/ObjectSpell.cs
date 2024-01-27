using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpell : AbstractSpell
{
    [SerializeField]
    GameObject objPrefab;

    public override void Execute()
    {
        GameObject poolObj = Instantiate(objPrefab, transform.position, Quaternion.identity);

        AbstractPool pool = poolObj.GetComponent<AbstractPool>();

        pool.AssignPlayer(owner);
    }

    protected override void ChildTick()
    {
        
    }
}
