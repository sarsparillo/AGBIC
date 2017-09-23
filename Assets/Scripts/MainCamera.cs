using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
	
	public GameObject player;
	[Tooltip("Minimum 1.1, as camera should move faster than player.")]
	[Range(1,5)]
	public float speed;
	private Vector3 playerPosition, previousPlayerPosition, cameraPosition;

	[Space(10)]
	[Header("Initialise with first room of level")]
	public BoxCollider2D levelCollider;
	private Vector3 bottomRight,topLeft;

	void Start () {
		cameraPosition = transform.position;
		UpdateBounds();
		Debug.Log(bottomRight + " " + topLeft);
	}
	
	void LateUpdate () {
		CameraPosition();
	}

	void CameraPosition() {
		playerPosition = player.transform.position;

		// Only update if player has moved.
		if (playerPosition != previousPlayerPosition) {
			cameraPosition = transform.position;

			// Get distance of player from camera to let camera play 'catchup'
			Vector3 playerPositionDiff = playerPosition - previousPlayerPosition;

			// Move the camera this direction faster than the player moved
			Vector3 multipliedDifference = playerPositionDiff * speed;

			cameraPosition += multipliedDifference;
		}
	}







	void UpdateBounds() {
		bottomRight = levelCollider.bounds.min;
		topLeft = levelCollider.bounds.max;
	}

	public void NewLevel(BoxCollider2D level) {
		levelCollider = level;
		UpdateBounds();
	}
}
