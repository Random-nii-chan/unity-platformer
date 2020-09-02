using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CombatAI))]
[RequireComponent(typeof(Rigidbody2D))]
public class HellbeastMovement : MovementAI
{
    private enum State
	{
        Idle,
        Attacking
	}

    [Header("Player detection")]
    public Transform raycastOrigin;
    public float playerDetectionRadius;
    //private variables
    [SerializeField] bool inSight;
    [SerializeField] State state;

    //components
    CombatAI comb;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        comb = GetComponent<CombatAI>();
        rb = GetComponent<Rigidbody2D>();
        GetPlayer();
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = GetPlayerDistance();
        inSight = !LineOfSight();

        if(inSight && playerDistance < playerDetectionRadius) {
            state = State.Attacking;
		} else {
            state = State.Idle;
		}

        comb.decreaseTimer = (state == State.Attacking);

        rb.velocity = new Vector2(0,rb.velocity.y);

        switch(state) {
            case State.Attacking:
                AttackPlayer();
                break;

            default:

                break;
		}
    }

	private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,playerDetectionRadius);
    }

	void AttackPlayer() {
        if (transform.position.x < player.position.x) {
            if (!facingRight && canFlip) Flip();
        }
        else {
            if (facingRight && canFlip) Flip();
        }
        comb.Attack();
    }

    public bool LineOfSight() {
        return Physics2D.Linecast(raycastOrigin.position, player.position, whatAreObstacles);
    }
}
