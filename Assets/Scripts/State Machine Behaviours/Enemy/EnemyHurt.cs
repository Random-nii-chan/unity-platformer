using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : StateMachineBehaviour
{
    GameObject enemy;

    //components
    CombatAI comb;
    MovementAI move;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        comb = enemy.GetComponent<CombatAI>();
        move = enemy.GetComponent<MovementAI>();

        move.canMove = false;
        move.canFlip = false;
        comb.canAttack = false;
        comb.decreaseTimer = false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move.canMove = true;
        move.canFlip = true;
        comb.canAttack = true;
        comb.decreaseTimer = true;
    }
}
