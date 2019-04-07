using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public bool isDead = false;
    public float health;

    private Animator animator;
    private BoxCollider2D box;

	void Awake () {
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }
	
    public virtual void Damage(float damage) {
        health -= damage;

        if (health <= 0) {
            isDead = true;
            Die();
        }
    }

    public virtual void Die() {
        animator.SetBool("isDead", true);
        box.enabled = false;
    }
}
