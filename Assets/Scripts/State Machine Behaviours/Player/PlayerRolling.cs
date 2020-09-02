using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRolling : StateMachineBehaviour
{
    [Header("Layers")]
    public int playerLayer = 8;
    public int enemyLayer = 10;

    [Header("Rolling attributes")]
    public float beginRollSpeed;
    public float rollSpeedDropMultiplier;
    public float rollSpeedMinimum;

    GameObject player;

    //components
    Player dmg;
    PlayerController cont;
    PlayerCombat comb;
    GhostTrail ghost;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cont = player.GetComponent<PlayerController>();
        comb = player.GetComponent<PlayerCombat>();
        ghost = player.GetComponent<GhostTrail>();
        dmg = player.GetComponent<Player>();

        cont.rollInput = cont.facingRight ?  1 : -1;
        cont.rollSpeed = beginRollSpeed;

        cont.state = PlayerController.State.Rolling;
        comb.canAttack = false;
        dmg.canTakeDamage = false;
        ghost.createGhost = true;
        Physics2D.IgnoreLayerCollision(playerLayer,enemyLayer,true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cont.rollSpeed -= cont.rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cont.state = PlayerController.State.Normal;
        comb.canAttack = true;
        ghost.createGhost = false;
        dmg.canTakeDamage = true;
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
    }
}
