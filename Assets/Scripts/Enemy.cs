using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private SpawnEnemies room;

	void Start() {
		room = GetComponentInParent<SpawnEnemies>();
	}
		

	void OnDestroy() {
		room.EnemyKilled();
	}
}
