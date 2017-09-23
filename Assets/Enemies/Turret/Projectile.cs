using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	public float damage;
	
	void FixedUpdate () {
		transform.Translate(Vector2.left * speed * Time.deltaTime);
	}

	void DestroyBullet() {
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Player")) {
			Health health = col.gameObject.GetComponent<Health>();
			health.DoDamage(damage);
		}
		DestroyBullet();
	}
}
