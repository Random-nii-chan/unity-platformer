using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collision))]
[RequireComponent(typeof(Animator))]
public class PlayerCombat : MonoBehaviour
{
    [Header("General Info")]
    public bool canAttack = true;
    public Transform attackPoint;
    public float timeBeforeNextAttack = 0.5f;
    public LayerMask whatAreEnemies;
    //private variables
    private float attackCooldown;
    [HideInInspector] public float attackRange = 0.5f;

    [Header("Combo chain parameters")]
    public int maxChain = 3;
    //private variables
    private int currentChainPosition = 1;

    [Header("Damage Info")]
    public float baseDamage;

    //components
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attackCooldown = 0;
    }

    private void Update() {
        attackCooldown -= Time.deltaTime; 
        if (Input.GetKeyDown(KeyCode.E) && canAttack) {
            if (attackCooldown < 0) {
                Attack(true);
                attackCooldown = timeBeforeNextAttack;
            }
        }
    }

    public void Attack(bool canIncreaseCombo) {
        if (currentChainPosition == maxChain) {
            currentChainPosition = 0;
        }
        if (canIncreaseCombo) { 
            currentChainPosition++;
        }
        //handling animation components
        anim.SetTrigger("Attack");
        anim.SetInteger("AttackChain", currentChainPosition);
    }

    public void HurtEnemies() {
        //detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatAreEnemies);
        //Damage them
        foreach (Collider2D enemy in hitEnemies) {
            enemy.GetComponent<Enemy>().TakeDamage(returnNormalDamage(currentChainPosition));
        }
    }

    private float returnNormalDamage(int chainPos) {
        return baseDamage;
    }

    private void OnDrawGizmos() {
        if (attackPoint == null) return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
