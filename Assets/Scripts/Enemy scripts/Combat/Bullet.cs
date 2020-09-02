using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public float speed;
    public bool facingRight;
    [Space]
    public float damage;

    //components
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (facingRight ? transform.right : -transform.right) * speed;
    }

	private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.GetComponent<Player>();
        if(player != null) {
            player.TakeDamage(damage);
		}
        Destroy(gameObject);
	}
}
