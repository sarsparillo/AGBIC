using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
	
	[Tooltip("Higher speed means camera snaps faster to the player position.")]
	[Range(2,8)]
	public float cameraSnapSpeed = 4;

	[Space(10)]
	[Header("Initialise with first room of level")]
	public BoxCollider2D levelCollider;

	private GameObject player;
	private Vector3 playerPosition, cameraPosition;
	// borders of current map
	private float bottomMost, topMost, leftMost, rightMost;

	private Camera camera;
	private float cameraHorizontal, cameraVertical;

	private bool transitioning;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		camera = GetComponent<Camera>();
		cameraPosition = transform.position;
		cameraVertical = camera.orthographicSize;
		cameraHorizontal = camera.orthographicSize * camera.aspect;

		UpdateBounds();
	}
	
	void LateUpdate () {
		if (!transitioning) {
			CameraPosition();
		} else {
			RoomTransition();
		}
	}

	void CameraPosition() {
		playerPosition = player.transform.position;
		playerPosition.y = playerPosition.y + 2;
		cameraPosition = transform.position;

		cameraPosition = Vector3.Lerp(cameraPosition, playerPosition, cameraSnapSpeed * Time.deltaTime);

		cameraPosition.x = Mathf.Clamp(cameraPosition.x, leftMost, rightMost);
		cameraPosition.y = Mathf.Clamp(cameraPosition.y, bottomMost, topMost);

		cameraPosition.z = -10;

		transform.position = cameraPosition;
	}

	void UpdateBounds() {
		bottomMost = levelCollider.bounds.min.y + cameraVertical;
		topMost = levelCollider.bounds.max.y - cameraVertical;
		leftMost = levelCollider.bounds.min.x + cameraHorizontal;
		rightMost = levelCollider.bounds.max.x - cameraVertical;
		transitioning = true;
	}

	void RoomTransition() {
		playerPosition = player.transform.position;
		playerPosition.y = playerPosition.y + 2;
		cameraPosition = transform.position;

		cameraPosition = Vector3.Lerp(cameraPosition, playerPosition, (cameraSnapSpeed * 2) * Time.deltaTime);

		cameraPosition.y = Mathf.Clamp(cameraPosition.y, bottomMost, topMost);
		cameraPosition.z = -10;

		transform.position = cameraPosition;
		if (cameraPosition.x > leftMost && cameraPosition.y < rightMost) {
			transitioning = false;
		}
	}

	public void NewLevel(BoxCollider2D level) {
		levelCollider = level;
		UpdateBounds();
	}
}
