using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanTakeDamage
{
    void TakeDamage(float damage, InvulnTypes invulnType);
}

public enum InvulnTypes
{
    FULLINVULN,
    DASHINVULN,
    IGNOREINVULN
}