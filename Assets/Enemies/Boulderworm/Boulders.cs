using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulders : MonoBehaviour {

	public float speed;
	public float damage;

	private Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(transform.localScale.x * (Random.Range(speed - 5f, speed + 5f)), 5f);
	}


	void DestroyBoulder() {
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Player")) {
			Health health = col.gameObject.GetComponent<Health>();
			health.DoDamage(damage);
		}
		DestroyBoulder();
	}
}
