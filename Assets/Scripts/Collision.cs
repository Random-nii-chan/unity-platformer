using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Collision settings")]
    public LayerMask whatIsGround;

    [Header("Ground collision")]
    public bool onGround;
    public Transform groundCheck;
    public Vector2 groundCheckSize;

    [Header("Ceiling collision")]
    public bool toCeiling;
    public Transform ceilingCheck;
    public Vector2 ceilingCheckSize;

	private void Update() {
        CheckGround();
        CheckCeiling();
	}

    private void CheckCeiling() {
        toCeiling = Physics2D.OverlapBox(ceilingCheck.position, ceilingCheckSize, 0f, whatIsGround);
	}

	private void CheckGround() {
        onGround = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, whatIsGround);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        Gizmos.DrawWireCube(ceilingCheck.position, ceilingCheckSize);
    }
}
