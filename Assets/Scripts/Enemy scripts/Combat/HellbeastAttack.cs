using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HellbeastAttack : CombatAI
{
    [Header("Attack properties")]
    public Transform firePoint;
    public float timeBeforeNextAttack;
    //private 
    private float timerBeforeNextAttack;

    [Header("Projectile info")]
    public GameObject projectile;

    //components
    Animator anim;

	// Start is called before the first frame update
	void Start()
    {
        anim = GetComponent<Animator>();
        timerBeforeNextAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(decreaseTimer) timerBeforeNextAttack -= Time.deltaTime;
    }

    public override void Attack() {
        if(timerBeforeNextAttack < 0 && canAttack) {
            timerBeforeNextAttack = timeBeforeNextAttack;
            anim.SetTrigger("Attacking");
		}
    }

    private void Shoot() {
        Instantiate(projectile, firePoint.position, firePoint.rotation);
	}
}
