using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatAI : MonoBehaviour
{
    [Header("Combat AI general Info")]
    public bool canAttack;
    public bool attacking;
    public bool decreaseTimer;

    public float attackRange;

    public abstract void Attack();
}
