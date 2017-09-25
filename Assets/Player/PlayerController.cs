using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour {

	[Header("Gotta public these up because of bugs")]
	public bool facingRight = true;
	public bool isJumping = false;
	public bool isAttacking = false;
	public bool isDashing = false;
	public bool isHit = false;
	public bool playerHasLanded = false;


	[Header("Camera")]
	public MainCamera gameCamera;
	public BoxCollider2D respawnRoom;
	public Vector3 respawnPosition;
	public float fallZone;

	[Header("Movement Details")]
	[Tooltip("Horizontal axis multiplier. Default to 200.")]
	[Range(100,500)]
	public float axisMultiplier;
	[Tooltip("Maximum player speed. Default is 6.5")]
	[Range(0,10)]
	public float maxSpeed;
	[Tooltip("Amount of force applied to the start of the jump. More force = more jump. Default is 1160")]
	[Range(800,14000)]
	public float jumpForce;
	[Tooltip("Distance the player backdashes. Default is 12")]
	[Range(0,20)]
	public float backDashDistance;
	[Tooltip("How much knockback when hit by an enemy")]
	public float hitKnockbackVertical;
	public float hitKnockbackHorizontal;

	private float currentSpeed;
	private Animator anim;
	private Rigidbody2D rb;


	void Awake() {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		currentSpeed = maxSpeed;
		respawnPosition = transform.position;
	}

	void Update () {
		if (Input.GetButtonDown("Jump")) {
			Jump();
		}

		if (Input.GetButtonDown("Attack")) {
			Attack();
		}

		if (Input.GetButtonDown("Dash")) {
			Dash();
		}

		// if you haven't landed from a jump or fall, start running a check to see if you've made contact with the ground again
		Vector2 groundCheck = transform.position;
		groundCheck.y -= 0.1f;
		Debug.DrawLine(transform.position, groundCheck, Color.cyan);

		playerHasLanded = Physics2D.Linecast(transform.position, groundCheck, 1 << LayerMask.NameToLayer("Ground"));
		if (playerHasLanded) {
			anim.SetBool("TouchingGround", true);
		} else {
			anim.SetBool("TouchingGround", false);
		}

		float horizontalForce = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(horizontalForce));

		// add directional force if not already at max speed
		if (horizontalForce * rb.velocity.x < maxSpeed) {
			if (!isDashing) {
				rb.AddForce(Vector2.right * horizontalForce * axisMultiplier);
			}
		}

		// directional force check, attempt to not let player slide up hills
		if (rb.velocity.y > 0 && !isJumping) {
			Debug.Log("Going Uphill");
		}
			

		// flip if you're moving in the opposite direction to what you're facing
		if (horizontalForce > 0 && !facingRight) {
			Flip();
		} else if (horizontalForce < 0 && facingRight) {
			Flip();
		}

		// speed trap!
		if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
			rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
		}

		if (isDashing) {
			if (facingRight) {
				rb.velocity = new Vector2(-backDashDistance, rb.velocity.y);
			} else {
				rb.velocity = new Vector2(backDashDistance, rb.velocity.y);
			}
		}

		if (isHit) {
			if (facingRight) {
				rb.velocity = new Vector2(rb.velocity.x - hitKnockbackHorizontal, rb.velocity.y + hitKnockbackVertical);
			} else {
				rb.velocity = new Vector2(rb.velocity.x + hitKnockbackHorizontal, rb.velocity.y + hitKnockbackVertical);
			}
		}

		// Respawn if dropped below map
		if (transform.position.y < fallZone) {
			Respawn();
		}

			
	}

	void Jump()	{
		if (playerHasLanded && !isAttacking && !isDashing) {
			anim.SetTrigger("Jump");
			rb.AddForce(transform.up * jumpForce);
			isJumping = true;
		}
	}

	// change direction; flip with scale by multiplying by negative 1
	void Flip() {
		if (!isAttacking && !isDashing) {
			facingRight = !facingRight;
			Vector3 transformScale = transform.localScale;
			transformScale.x *= -1;
			transform.localScale = transformScale;
		}
	}

	void Attack() {
		if (!isAttacking && !isDashing && !isHit) {
			isAttacking = true;
			if (playerHasLanded) {		
				anim.SetTrigger("Attack");
				maxSpeed = 0;
			} else {
				anim.SetTrigger("AirAttack");
			}
		}
	}

	void EndAttack() {
		isAttacking = false;
		isDashing = false;
		maxSpeed = currentSpeed;
	}


	void Dash() { 
		if (!isDashing && !isAttacking && playerHasLanded) {
			isDashing = true;
			isJumping = false;
			anim.SetBool("Dashing", true);
		}
	}

	void EndDash() {
		isDashing = false;
		isAttacking = false;
		isJumping = false;
		anim.SetBool("Dashing", false);
	}

	public void TakeHit(float damage) {
		if (!isDashing && !isHit) {
			isHit = true;
			isDashing = false;
			isAttacking = false;
			isJumping = false;
			Health health = GetComponent<Health>();
			health.DoDamage(damage);
		}
	}

	void EndTakeHit() {
		isHit = false;		
	}

	public void Respawn() {
		transform.position = respawnPosition;
		gameCamera.NewLevel(respawnRoom);
		isHit = false;
		isDashing = false;
		isAttacking = false;
		isJumping = false;
	}


	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("LevelBox")) {
			BoxCollider2D level = col.GetComponent<BoxCollider2D>();
			gameCamera.GetComponent<MainCamera>().NewLevel(level);
		}
		if (col.gameObject.CompareTag("Enemy")) {
			Health health = GetComponent<Health>();
			health.DoDamage(10f);
		}
	}

	public void UpdateRespawnPosition(Vector3 newPosition, BoxCollider2D room) {
		respawnPosition = newPosition;
		respawnRoom = room;
	}

}
