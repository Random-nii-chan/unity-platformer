using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collision))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GhostTrail))]
public class PlayerController : MonoBehaviour
{
    public enum State{
        Normal,
        Rolling,
        Attacking,
        Hurt,
        Knocked
    }

    [Header("Movement attributes")]
    public float speed;
    public bool facingRight = true;
    //private variables
    private float moveInput;
    public State state;

    //[Header("Rolling attributes")]
    [HideInInspector] public float rollInput;
    [HideInInspector] public float rollSpeed;

    [Header("Jumping attributes")]
    public float jumpForce;
    public float jumpTime;
    //private variables
    private float jumpTimeCounter;
    private bool isJumping;

    [Header("Knockback")]
    public bool activateKnockback = true;
    public float duration;
    public float knockbackForce;

    //components
    private Rigidbody2D rb;
    private Collision coll;
    private Animator anim;
    private PlayerCombat comb;

    private void Awake() {
        state = State.Normal;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
        anim = GetComponent<Animator>();
        comb = GetComponent<PlayerCombat>();
    }

    private void Update() {
        switch (state) {
            case State.Normal:
                GetInput();
                Move();
                break;

            case State.Rolling:
                MoveRoll();
                break;

            default:
                break;
        }
    }

	private void LateUpdate() {
        anim.SetFloat("HorizontalSpeed", Mathf.Abs(moveInput));
        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        anim.SetBool("OnGround", coll.onGround);
    }

    private void GetInput() {
        moveInput = Input.GetAxis("Horizontal");

        if (!facingRight && moveInput > 0) { 
            //si on regarde vers la gauche et qu'on va vers la droite
            Flip();
        }
        else if (facingRight && moveInput < 0) { 
            //si on regarde vers la droite et qu'on va vers la gauche
            Flip();
        }

        if (coll.onGround) {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping) {
                anim.SetTrigger("Roll");
            }
        }
        comb.canAttack = coll.onGround;

		if (coll.onGround && Input.GetKeyDown(KeyCode.UpArrow)) {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            Jump();
		}

        if(Input.GetKey(KeyCode.UpArrow) && isJumping) {
            if(jumpTimeCounter > 0) {
                Jump();
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
			}
        }

        if(Input.GetKeyUp(KeyCode.UpArrow) || coll.toCeiling) {
            isJumping = false;
		}
    }

    private void Move() {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void MoveRoll() {
        rb.velocity = new Vector2(rollInput * rollSpeed, rb.velocity.y);
    }

    private void Flip() {
        facingRight = !facingRight;
        transform.localScale *= new Vector2(-1,1);
    }

    private void Jump() {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
