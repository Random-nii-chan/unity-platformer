using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : StateMachineBehaviour
{
    GameObject enemy;

    //components
    Rigidbody2D rb;
    MovementAI move;
    CombatAI comb;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        rb = enemy.GetComponent<Rigidbody2D>();
        move = enemy.GetComponent<MovementAI>();
        comb = enemy.GetComponent<CombatAI>();

        rb.velocity = new Vector2(0,rb.velocity.y);
        move.canFlip = false;
        comb.attacking = true;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move.canFlip = true;
        comb.attacking = false;
    }
}
