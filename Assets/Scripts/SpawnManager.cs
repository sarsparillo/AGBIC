using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[Tooltip("Number of rooms before a respawn room is created")]
	public int roomsBetweenRespawnPoints;
	public GameObject respawnRoom;
	public GameObject[] rooms;

	private Vector2 originPosition, nextPosition;
	private float respawnWidth;

	void Start () {
		originPosition = transform.position;
		respawnWidth = respawnRoom.GetComponent<Tiled2Unity.TiledMap>().NumTilesWide;
		Spawn();
	}

	//
	public void Spawn() {
		for (int i = 0; i <= roomsBetweenRespawnPoints; i++) {
			int roomChoice = Random.Range(0, rooms.Length);
			GameObject room = rooms[roomChoice];
			nextPosition = originPosition;
			Instantiate(room, nextPosition, Quaternion.identity);
			// define width, add this to the next position
			float width = room.GetComponent<Tiled2Unity.TiledMap>().NumTilesWide;
			nextPosition.x += width;
			originPosition = nextPosition;
		}
		CreateRespawnRoom();
	}

	void CreateRespawnRoom() {
		Instantiate(respawnRoom, nextPosition, Quaternion.identity);
		nextPosition.x += respawnWidth;
		originPosition = nextPosition;		
	}
}