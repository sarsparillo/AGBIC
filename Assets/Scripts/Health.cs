using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float health;
	private HealthBar healthBar;
	private Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
		healthBar = GetComponent<HealthBar>();
	}

	public void DoDamage(float damage) {
		health -= damage;
		if (healthBar) {
			healthBar.UpdateHealth(health);
		}
		if (health > 0) {
			anim.SetTrigger("Hit");
		} else {
			anim.SetTrigger("Die");
		}
	}

	public void Die() {
		Destroy(gameObject);
	}

}
