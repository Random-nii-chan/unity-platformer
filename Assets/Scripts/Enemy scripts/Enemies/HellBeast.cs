using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementAI))]
[RequireComponent(typeof(CinemachineImpulseSource))]
public class HellBeast : Enemy
{
    [Header("Death effect")]
    public Transform instantiatePoint;
    public GameObject deathEffect;

    //components
    protected Collider2D col;
    protected Rigidbody2D rb;
    protected MovementAI move;
    protected Animator anim;
    protected CinemachineImpulseSource impulse;

    // Start is called before the first frame update
    new void Start() {
        base.Start();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<MovementAI>();
        impulse = GetComponent<CinemachineImpulseSource>();
    }

	new private void Update() {
        base.Update();
        if (Input.GetKeyDown(KeyCode.T)) {
            ShakeScreen();
		}
	}

	protected void Die() {
        anim.SetTrigger("Dying");
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.isKinematic = true;
        col.enabled = false;
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

	public void InstantiateDeathEffect() {
        GameObject de = Instantiate(deathEffect, instantiatePoint.position, Quaternion.identity);
    }

    public void ShakeScreen() {
        impulse.GenerateImpulse();
	}
}
