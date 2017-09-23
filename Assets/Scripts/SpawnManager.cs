using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public int maxRooms, generatedRooms;
	public GameObject[] platforms;

	private Vector2 originPosition;

	void Start () {
		originPosition = transform.position;
		for (int i = 0; i <= maxRooms; i++) {
			Spawn();
		}
	}
	
	void Spawn() {
		int platformChoice = Random.Range(0, platforms.Length);
		GameObject platform = platforms[platformChoice];
		Vector2 nextPosition = originPosition;
		Instantiate(platform, nextPosition, Quaternion.identity);
		// define width, add this to the next position
		float width = platform.GetComponent<Tiled2Unity.TiledMap>().NumTilesWide;
		nextPosition.x += width;
		originPosition = nextPosition;
	}
}