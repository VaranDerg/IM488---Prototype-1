using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanTakeDamage
{
    void Damage(float damage, InvulnTypes invulnType);
}

public enum InvulnTypes
{
    FULLINVULN,
    DASHINVULN,
    IGNOREINVULN
}