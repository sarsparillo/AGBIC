using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

	public float interpVelocity;
	public float minDistance;
	public float followDistance;
	public GameObject target;
	public GameObject level;
	public Vector3 offset;

	private Vector3 targetPos;
	private Vector3 minCamera;
	private Vector3 maxCamera;
	// Use this for initialization
	void Awake () {
		targetPos = transform.position;
		SetNewLevelBounds(level);
	}

	public void SetNewLevelBounds(GameObject obj) {
		level = obj;
		Tiled2Unity.TiledMap bounds = obj.GetComponent<Tiled2Unity.TiledMap>();
		Rect boundsRect = bounds.GetMapRect();
		minCamera = new Vector3(boundsRect.x, boundsRect.y, transform.position.z);
		maxCamera = new Vector3((boundsRect.xMax / 2), (boundsRect.yMax / 2), transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (target)	{
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;

			Vector3 targetDirection = (target.transform.position - posNoZ);

			interpVelocity = targetDirection.magnitude * 15f;

			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 

			transform.position = Vector3.Lerp( transform.position, targetPos + offset, 0.5f);
		}

		transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCamera.x, maxCamera.x), Mathf.Clamp(transform.position.y, minCamera.y, maxCamera.y), transform.position.z);
	}
}
