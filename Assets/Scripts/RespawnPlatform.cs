using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlatform : MonoBehaviour {

	private Animator anim;
	private SpawnManager roomManager;
	private BoxCollider2D room;

	void Start() {
		anim = GetComponent<Animator>();
		roomManager = GameObject.FindObjectOfType<SpawnManager>();
		room = GetComponentInParent<BoxCollider2D>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Player")) {
			anim.SetTrigger("Activate");
			roomManager.Spawn();
			col.gameObject.GetComponent<PlayerController>().UpdateRespawnPosition(transform.position, room);
		}
	}
}
