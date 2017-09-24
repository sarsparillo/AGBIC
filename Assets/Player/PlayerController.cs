using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool isJumping = false;
	[HideInInspector] public bool isAttacking = false;

	[Header("Camera")]
	public GameObject gameCamera;

	[Header("Level and Respawn Positions")]
	public BoxCollider2D currentRoomBounds;
	public Vector3 respawnPosition;
	public float fallZone;

	[Header("Movement Details")]
	public float moveForce;
	public float maxSpeed;
	public float jumpForce;
	public float backDashDistance;
	[Tooltip("Location of Y coordinate to do linecasting to read the ground position")]
	public Transform groundCheck;

	private bool playerHasLanded = false;
	private bool isDashing = false;
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
	}

	void FixedUpdate() {
		float horizontalForce = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(horizontalForce));

		// add directional force if not already at max speed
		if (horizontalForce * rb.velocity.x < maxSpeed) {
			if (!isDashing) {
				rb.AddForce(Vector2.right * horizontalForce * moveForce);
			}
		}

		// directional force check, can't slide up hills
		if (rb.velocity.y > 0 && !isJumping) {
			rb.AddForce(Vector2.right * -horizontalForce);
		}
			

		// flip if you're moving in the opposite direction to what you're facing
		if (horizontalForce > 0 && !facingRight) {
			Flip();
		} else if (horizontalForce < 0 && facingRight) {
			Flip();
		}

		// speed trap!
		if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
			// mathf.sign returns 1 if positive, -1 if negative, 
			rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
		}

		// if you haven't landed from a jump or fall, start running a check to see if you've made contact with the ground again
		playerHasLanded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if (!playerHasLanded) {
			anim.SetBool("TouchingGround", false);
		} else {
			anim.SetBool("TouchingGround", true);
		}


		if (isDashing) {
			if (facingRight) {
				rb.velocity = new Vector2(-backDashDistance, rb.velocity.y);
			} else {
				rb.velocity = new Vector2(backDashDistance, rb.velocity.y);
			}
		}

		// Respawn if dropped below map
		if (transform.position.y < fallZone) {
			Respawn();
		}
			
	}

	void Jump()	{
		if (playerHasLanded && !isAttacking) {
			isJumping = true;
			anim.SetTrigger("Jump");
			rb.AddForce((transform.up) * jumpForce);
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
		if (!isAttacking && !isDashing) {
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
		if (!isAttacking && playerHasLanded && !isDashing) {
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

	public void Respawn() {
		transform.position = respawnPosition;
	}

	// if exited ground collider
	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.CompareTag("Ground")) {
			anim.SetBool("TouchingGround", false);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("LevelBox")) {
			BoxCollider2D level = col.GetComponent<BoxCollider2D>();
			gameCamera.GetComponent<MainCamera>().NewLevel(level);
		}
	}

	public void UpdateRespawnPosition(Vector3 newPosition) {
		respawnPosition = newPosition;
	}

}
