using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementAI))]
public class Skeleton : Enemy
{
    //components
    protected Collider2D col;
    protected Rigidbody2D rb;
    protected MovementAI move;
    protected Animator anim;

    // Start is called before the first frame update
    new void Start() {
        base.Start();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<MovementAI>();
    }

    // Update is called once per frame
    new void Update() {
        base.Update();
        if(Input.GetKeyDown(KeyCode.R) && isDead) {
            Revive();
		}
    }

    protected void Die() {
        //Die animation
        anim.SetBool("IsDead", true);
        anim.SetTrigger("Dying");
        rb.isKinematic = true;
        col.enabled = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        move.enabled = false;
    }

    public override void TakeDamage(float amount) {
        if (canTakeDamage) {
            hitPoints -= amount;
            //play hurtAnimation
            if (hitPoints <= 0) {
                Die();
            }
            else {
                anim.SetTrigger("Hurt");
            }
        }
    }

    void Revive() {
        col.enabled = true;
        rb.isKinematic = false;
        move.enabled = true;
        hitPoints = maxHitPoints;
        anim.SetBool("IsDead", false);
        anim.SetTrigger("Reviving");
    }

    public void ScreenShake() {
        GetComponent<CinemachineImpulseSource>().GenerateImpulse();
	}
}
