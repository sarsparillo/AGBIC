using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

	public Transform[] spawnPoints;
	public GameObject[] enemies;
	private Door door;

	public int numEnemies;

	void Start () {
		door = GetComponentInChildren<Door>();
		Spawn();
	}

	void Spawn() {
		for (int i = 0; i < spawnPoints.Length; i++) {
			int coinFlip = Random.Range(0, 2);
			if (coinFlip > 0) {
				int enemyChoice = Random.Range(0, enemies.Length);
				GameObject enemy = enemies[enemyChoice];
				Instantiate(enemy, spawnPoints[i].position, Quaternion.identity);
				numEnemies++;
			}
		}
	}

	void Update() {
		if (numEnemies < 1) {
			Debug.Log("No more enemies");
			door.GetComponent<Door>().OpenDoor();
		}
	}

	public void EnemyKilled() {
		numEnemies -= 1;
	}
}
