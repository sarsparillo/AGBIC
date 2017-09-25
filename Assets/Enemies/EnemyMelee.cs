using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour {
	
	public float damage;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Player")) {
			PlayerController player = col.gameObject.GetComponent<PlayerController>();
			player.TakeHit(damage);
		}
	}
}
