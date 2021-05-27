using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

	[SerializeField] private float jumpForce = 400f;
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
	[SerializeField] private bool airControl = false;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private Transform ceilingCheck;

	const float groundedRadius = .2f;
	private bool grounded;
	private bool inWater;
	const float ceilingRadius = .2f;
	private Rigidbody2D rigidbody;
	private bool facingRight = true;
	private Vector3 velocity = Vector3.zero;
	private PartsTracker tracker;
	private bool active;

	[Header("Events")]
	[Space]

	private Animator anim;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		tracker = GetComponent<PartsTracker>();
		active = true;
	}

	private void FixedUpdate()
	{
		bool wasGrounded = grounded;
		grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;
			}
		}
		anim.SetBool("IsFalling", !grounded);
	}

	public void Move(float move, bool jump)
	{
		if (active)
		{
			if (grounded || airControl)
			{
				Vector3 targetVelocity = new Vector2(move * 10f, rigidbody.velocity.y);
				rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, movementSmoothing);

				if (move > 0 && !facingRight)
				{
					Flip();
				}
				else if (move < 0 && facingRight)
				{
					Flip();
				}
			}

			if (grounded && jump)
			{
				grounded = false;
				rigidbody.AddForce(new Vector2(0f, jumpForce));
			}
		}
	}

	public bool isFalling()
	{
		return !grounded;
	}

	public bool InWater()
	{
		return inWater;
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Water" && tracker.Floats())
		{
			inWater = true;
			anim.SetBool("Water", inWater);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Water")
		{
			Debug.Log("Leaving Water");
			inWater = false;
			anim.SetBool("Water", inWater);
		}
	}

	private void Flip()
	{
		facingRight = !facingRight;
		GetComponent<SpriteRenderer>().flipX = !facingRight;
	}

	public bool IsFacingRight()
	{
		return facingRight;
	}

	public void SetMovement(bool activeSetter)
	{
		active = activeSetter;
	}
}