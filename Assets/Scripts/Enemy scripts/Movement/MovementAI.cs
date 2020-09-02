using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class MovementAI : MonoBehaviour
{
    [Header("Movement AI general info")]
    public bool canFlip;
    public bool canMove;
    public string playerTag = "Player";

    [SerializeField] protected bool facingRight;
    [SerializeField] protected LayerMask whatAreObstacles;
    protected Transform player;
    protected float playerDistance;

	public void Flip() {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
	}

    public void GetPlayer() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public float GetPlayerDistance() {
        return Vector2.Distance(transform.position,player.position);
    }
}
