using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool drawDebugRaycasts = true;

	public float speed = 8f;
	public float Currentspeed;
	public float coyoteDuration = .05f;
	public float maxFallSpeed = -25f;

	public float jumpForce = 6.3f;
	public float jumpHoldForce = 1.9f;
	public float jumpHoldDuration = .1f;
	
	public float footOffset = .4f;
	public float groundDistance = .2f;
	public LayerMask groundLayer;
	
	public bool isOnGround;
	public bool isJumping;
	
	PlayerInput input;
	BoxCollider2D bodyCollider;
	Rigidbody2D rigidBody;
	
	float jumpTime;
	float coyoteTime;

	float originalXScale;
	int direction = 1;


	
	void Start ()
	{
		input = GetComponent<PlayerInput>();
		rigidBody = GetComponent<Rigidbody2D>();
		bodyCollider = GetComponent<BoxCollider2D>();

		originalXScale = transform.localScale.x;
	}

	void FixedUpdate()
	{
		PhysicsCheck();
		GroundMovement();		
		MidAirMovement();
		
		if(rigidBody.velocity.y > 15){
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, 15);
		}   
		
		if (!isOnGround)
		{
			transform.GetComponent<Animator>().SetBool("isJumping",true);
		}
		else
		{
			transform.GetComponent<Animator>().SetBool("isJumping", false);
		}
	}

	void PhysicsCheck()
	{
		isOnGround = false;

		RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, -0.1f), Vector2.down, groundDistance);
		RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, -0.1f), Vector2.down, groundDistance);
		if (leftCheck || rightCheck)
			isOnGround = true;
	}

	void GroundMovement()
	{
		float xVelocity = speed * input.horizontal;
		if (xVelocity * direction < 0f)
			TurnDirection();

		rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
		Currentspeed = Mathf.Abs(rigidBody.velocity.x);
		
		transform.GetComponent<Animator>().SetFloat("Speed", Currentspeed);

		if (isOnGround)
			coyoteTime = Time.time + coyoteDuration;
	}

	void MidAirMovement()
	{
		if (input.jumpPressed && !isJumping && (isOnGround || coyoteTime > Time.time))
		{
			isOnGround = false;
			isJumping = true;
			jumpTime = Time.time + jumpHoldDuration;
			rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
		}

		else if (isJumping)
		{
			if (input.jumpHeld)
				rigidBody.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
			if (jumpTime <= Time.time)
				isJumping = false;
		}
		
		if (rigidBody.velocity.y < maxFallSpeed)
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxFallSpeed);
	}

	void TurnDirection()
	{
		direction *= -1;
		Vector3 scale = transform.localScale;
		scale.x = originalXScale * direction;
		transform.localScale = scale;
	}
	
	RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
	{
		return Raycast(offset, rayDirection, length, groundLayer);
	}

	RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
	{
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);
		if (drawDebugRaycasts)
		{
			Color color = hit ? Color.red : Color.green;
			Debug.DrawRay(pos + offset, rayDirection * length, color);
		}
		return hit;
	}
}
