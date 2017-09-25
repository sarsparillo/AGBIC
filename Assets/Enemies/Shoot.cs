using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject[] projectiles;
	public GameObject gun;

	public float rangeStart, rangeEnd;

	private bool attacking;
	private GameObject projectileParent;
	private Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
		projectileParent = GameObject.Find("Projectile Manager");
		if (!projectileParent) {
			projectileParent = new GameObject("Projectile Manager");
		}
	}
	
	void Update () {

		Vector2 target = transform.position;
		Vector2 targetStart = transform.position;
		target.x -= rangeEnd * transform.localScale.x;
		targetStart.x -= rangeStart * transform.localScale.x;
		Debug.DrawLine(targetStart, target, Color.yellow);
		RaycastHit2D seePlayer = Physics2D.Linecast(targetStart, target, 1 << LayerMask.NameToLayer("Player"));
		if (seePlayer.collider != null) {
			if (seePlayer.collider.name == "Player") {
				anim.SetBool("Shooting", true);
			} else {
				anim.SetBool("Shooting", false);
			}
		}
	}

	public void Fire(int numProjectiles) {
		for (int i = 0; i < numProjectiles; i++) {
			GameObject projectile = projectiles[Random.Range(0, projectiles.Length)];
			GameObject bullet = Instantiate(projectile, gun.transform.position, Quaternion.identity) as GameObject;
			bullet.transform.localScale = -transform.localScale;
			bullet.transform.parent = projectileParent.transform;
		}
	}

	void EndFiring() {
		anim.SetBool("Shooting", false);
	}
}
