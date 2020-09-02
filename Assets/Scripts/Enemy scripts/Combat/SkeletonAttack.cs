using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SkeletonAttack : CombatAI
{
    [Header("Attack properties")]
    public LayerMask whatIsPlayer;
    public Transform attackPoint;
    public float hitboxSize;
    public float timeBeforeNextAttack;
    //private variables
    private float timerBeforeNextAttack;

    [Header("Damage info")]
    public int damage;

    [Header("Attack chain")]
    public int maxChain;
    //private variables
    private int currentChainPosition = 1;

    //components
    private Animator anim;
        
    // Start is called before the first frame update
    void Start()
    {
        timerBeforeNextAttack = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(decreaseTimer) timerBeforeNextAttack -= Time.deltaTime;
    }

	private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(attackPoint.position, hitboxSize);
    }

    public override void Attack() {
        if (timerBeforeNextAttack < 0 && canAttack) {
            if (currentChainPosition == maxChain) {
                currentChainPosition = 0;
            }
            currentChainPosition++;
            timerBeforeNextAttack = timeBeforeNextAttack;
            anim.SetTrigger("Attacking");
            anim.SetInteger("ChainPosition", currentChainPosition);
        }
    }

    private void Hurt() {
        //detect player
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, hitboxSize, whatIsPlayer);
        //inflict damage
        foreach (Collider2D enemy in hitEnemies) {
            enemy.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
