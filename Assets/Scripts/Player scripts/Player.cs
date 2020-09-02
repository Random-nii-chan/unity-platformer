using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [Header("Resources")]
    public float hitPoints;
    //private variables
    private float currentHealth;

    [Header("Damage reception settings")]
    public bool canTakeDamage = true;

    //components
    private Animator anim;
    private Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = hitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth < 0) {
            Respawn();
		}
    }

	public void TakeDamage(float amount) {
        if (canTakeDamage) {
            hitPoints -= amount;
            //play hurt animation
            if (hitPoints <= 0) {
                Respawn();
            }
            else {
                anim.SetTrigger("Hurt");
            }
        }
    }

    public void Respawn() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
