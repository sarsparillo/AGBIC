using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour {

	public Weapon weapon;
	private Animator anim;
	private float damage;

	void Start() {
		damage = weapon.GetComponent<Weapon>().weaponDamage;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Enemy")) {
			Health health = col.gameObject.GetComponent<Health>();
			health.DoDamage(damage);
		}
	}
}
