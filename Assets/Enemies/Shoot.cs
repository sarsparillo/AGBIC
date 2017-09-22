using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject[] projectiles;
	public GameObject gun, target;

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
		RaycastHit2D seePlayer = Physics2D.Linecast(transform.position, target.transform.position, 1 << LayerMask.NameToLayer("Player"));
		if (seePlayer.collider != null) {
			if (seePlayer.collider.name == "Player") {
				anim.SetBool("isAttacking", true);
			} else {
				anim.SetBool("isAttacking", false);
			}
		}
	}

	public void Fire(int numProjectiles) {
		float direction = transform.position.x;
		for (int i = 0; i < numProjectiles; i++) {
			GameObject projectile = projectiles[Random.Range(0, projectiles.Length)];
			GameObject bullet = Instantiate(projectile, gun.transform.position, Quaternion.identity) as GameObject;
			bullet.transform.localScale = -transform.localScale;
			bullet.transform.parent = projectileParent.transform;
		}
	}
}
