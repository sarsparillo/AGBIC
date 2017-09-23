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
				GameObject thisEnemy = Instantiate(enemy, spawnPoints[i].position, Quaternion.identity) as GameObject;
				thisEnemy.transform.parent = transform;
				numEnemies++;
			}
		}
	}

	void Update() {
		if (numEnemies < 1) {
			door.GetComponent<Door>().OpenDoor();
		}
	}

	public void EnemyKilled() {
		numEnemies -= 1;
	}
}
