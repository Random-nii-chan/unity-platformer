using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CombatAI))]
[RequireComponent(typeof(Rigidbody2D))]
public class SkeletonMovement : MovementAI
{
    public enum State
    {
        Idle,
        Following,
        Attacking
    }

    [Header("Movement")]
    public float moveSpeed;
    public State state;

    [Header("Player Detection")]
    public float playerDetectionRadius;
    public float deadzone;
    //private variables
    private float distanceToPlayer;

    //components
    private Animator anim;
    private CombatAI comb;
    private Rigidbody2D rb;

    private void Start() {
        state = State.Idle;
        anim = GetComponent<Animator>();
        comb = GetComponent<CombatAI>();
        rb = GetComponent<Rigidbody2D>();

        GetPlayer();

        canFlip = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update() {
        distanceToPlayer = GetPlayerDistance();
        bool inSight = LineOfSight();

        if (distanceToPlayer < comb.attackRange && inSight && !comb.attacking) {
            state = State.Attacking;
        }
        else if (distanceToPlayer < playerDetectionRadius && inSight && !comb.attacking) {
            state = State.Following;
        }
        else {
            state = State.Idle;
        }

        if (state == State.Attacking) comb.decreaseTimer = true;
        else comb.decreaseTimer = false;

        anim.SetFloat("HorizontalSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("VerticalSpeed", rb.velocity.y);
    }

    void FixedUpdate() {
        switch (state) {
            case State.Following:
                if (canMove) ChasePlayer();
                break;

            case State.Attacking:
                CancelMovement();
                AttackPlayer();
                break;

            default:
                CancelMovement();
                break;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }

    void ChasePlayer() {
        float xAxisDiff = Mathf.Abs(player.position.x - transform.position.x);
        if (xAxisDiff >= deadzone) {
            //enemy at the left side of the player
            if (transform.position.x < player.position.x) {
                if (!facingRight && canFlip) Flip();
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            //enemy at the right side of the player
            else {
                if (facingRight && canFlip) Flip();
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        }
    }

    void AttackPlayer() {
        if (transform.position.x < player.position.x) {
            if (!facingRight && canFlip) Flip();
            comb.Attack();
        }
        else {
            if (facingRight && canFlip) Flip();
            comb.Attack();
        }
    }

    public void CancelMovement() {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public bool LineOfSight() {
        return !Physics2D.Linecast(transform.position, player.position, whatAreObstacles);
    }
}
