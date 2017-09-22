using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour {

	void onTriggerEnter2D(Collider2D col) {
		Debug.Log("collided");
		if (col.gameObject.CompareTag("Player")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
