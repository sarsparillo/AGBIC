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
		Invoke("TimeOut", 3f);
	}

	void TimeOut() {
		DestroyBoulder();
	}


	void DestroyBoulder() {
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag("Player")) {
			PlayerController player = col.gameObject.GetComponent<PlayerController>();
			player.TakeHit(damage);
			DestroyBoulder();
		}
	}
}
