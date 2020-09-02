using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BetterJump : MonoBehaviour
{
    public float normalGravityScale = 1.0f;
    public float fallingGravityScale = 1.0f;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y < 0) {
            rb.gravityScale = fallingGravityScale;
        } else if(rb.velocity.y >= 0) {
            rb.gravityScale = normalGravityScale;
        }
    }
}
