using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : StateMachineBehaviour
{
    GameObject player;

    //components
    PlayerController cont;
    PlayerCombat comb;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cont = player.GetComponent<PlayerController>();
        comb = player.GetComponent<PlayerCombat>();
        rb = player.GetComponent<Rigidbody2D>();

        comb.canAttack = false;
        rb.velocity = new Vector2(0,rb.velocity.y);
        cont.state = PlayerController.State.Hurt;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cont.state = PlayerController.State.Normal;
        comb.canAttack = true;
    }
}
