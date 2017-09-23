using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour {
	
	public float damage;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Player")) {
			Health health = col.gameObject.GetComponent<Health>();
			health.DoDamage(damage);
		}
	}
}
