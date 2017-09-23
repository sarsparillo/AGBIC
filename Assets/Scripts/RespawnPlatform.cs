using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlatform : MonoBehaviour {

	public GameObject levelGenerator;
	private Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Player")) {
			anim.SetTrigger("Activate");
			levelGenerator.GetComponent<SpawnManager>().Spawn();
			col.gameObject.GetComponent<PlayerController>().UpdateRespawnPosition(transform.position);
		}
	}
}
