using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAI : MonoBehaviour {

	public float speed;
	public float maxSpeed;

	[Tooltip("How close the player needs to get before a physical attack is made")]
	[Range(0,5)]
	public float attackRange;
	[Range(0,3)]
	public float seeGroundPoint;
	[Range(0,3)]
	public float seeGroundRange;

	private bool facingRight, attacking, stopped;
	private Animator anim;
	private Rigidbody2D rb;

	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		stopped = false;
	}

	void FixedUpdate() {
		Vector2 attackCast = transform.position;
		attackCast.x -= attackRange * transform.localScale.x;
		RaycastHit2D attack = Physics2D.Linecast(transform.position, attackCast, 1 << LayerMask.NameToLayer("Player"));
		if (attack.collider != null) {
			if (attack.collider.name == "Player") {
				attacking = true;
				anim.SetBool("isWalking", false);
				anim.SetBool("Attack", true);
			} 

		} else {
			attacking = false;
			anim.SetBool("Attack", false);
		}


		if (!attacking && !stopped && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
			WalkForwards();
		}
	}

	void WalkForwards() {
		anim.SetBool("isWalking", true);
		if (Mathf.Abs(rb.velocity.x) < maxSpeed) {
			rb.AddForce(Vector2.left * speed * transform.localScale.x);
		} else {
			// mathf.sign returns 1 if positive, -1 if negative, 
			rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
		}

		// flip if there's no ground to walk forwards onto
		Vector2 seeGroundCheck = transform.position;
		seeGroundCheck.x -= seeGroundRange * transform.localScale.x;
		seeGroundCheck.y -= seeGroundPoint;
		Debug.DrawLine(transform.position, seeGroundCheck, Color.green);
		Debug.DrawLine(transform.position, new Vector2(seeGroundCheck.x, seeGroundCheck.y + 0.5f), Color.cyan);
		RaycastHit2D isFacingGround = Physics2D.Linecast(transform.position, seeGroundCheck, 1 << LayerMask.NameToLayer("Ground"));
		RaycastHit2D isTouchingObject = Physics2D.Linecast(transform.position, new Vector2(seeGroundCheck.x, seeGroundCheck.y + 0.5f));
		if (!isFacingGround) {
			Flip();
		}
		if (isTouchingObject.collider != null) {
			if (isTouchingObject.collider.gameObject.CompareTag("Enemy") || isTouchingObject.collider.gameObject.CompareTag("Ground")) {
				Debug.Log(isTouchingObject.collider.tag);
				Flip();
			}
		}
	}

	void StopMoving() {
		anim.SetBool("isWalking", false);
		stopped = true;
		Invoke("Reactivate", 1f);
	}

	void Reactivate() {
		stopped = false;
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 transformScale = transform.localScale;
		transformScale.x *= -1;
		transform.localScale = transformScale;
	}

}
