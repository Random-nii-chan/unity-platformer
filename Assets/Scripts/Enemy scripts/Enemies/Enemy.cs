using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    [Header("General Info")]
    public float maxHitPoints;
    [SerializeField] protected bool canTakeDamage = true;
    public UnityEvent deathEvents;
    [HideInInspector] public bool isDead = false;
    //private variables
    protected float hitPoints;

	protected void Start() {
        InitHitPoints();
        if(deathEvents == null) {
            deathEvents = new UnityEvent();
		}
	}

	protected void Update() {
        CheckIfDead();
    }

    protected void CheckIfDead() {
        isDead = (hitPoints <= 0);
    }

    protected void InitHitPoints() {
        hitPoints = maxHitPoints;
    }

	public abstract void TakeDamage(float amount);
}
