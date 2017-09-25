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
		Debug.Log(col.gameObject.name);
		if (col.gameObject.CompareTag("Player")) {
			PlayerController player = col.gameObject.GetComponent<PlayerController>();
			player.TakeHit(damage);
		}
		DestroyBullet();
	}
}
