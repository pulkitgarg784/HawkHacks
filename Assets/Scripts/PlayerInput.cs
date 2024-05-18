using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
	public bool testTouchControlsInEditor = false;
	public float verticalDPadThreshold = .5f;
	public Thumbstick thumbstick;
	public TouchButton jumpButton;

	[HideInInspector] public float horizontal;
	[HideInInspector] public bool jumpHeld;
	[HideInInspector] public bool jumpPressed;

	bool dPadCrouchPrev;
	bool readyToClear;
	
	void Update()
	{
		ClearInput();
		ProcessInputs();
		ProcessTouchInputs();
		horizontal = Mathf.Clamp(horizontal, -1f, 1f);
	}

	void FixedUpdate()
	{
		readyToClear = true;
	}

	void ClearInput()
	{
		if (!readyToClear)
			return;
		horizontal		= 0f;
		jumpPressed		= false;
		jumpHeld		= false;
		readyToClear	= false;
	}

	void ProcessInputs()
	{
		horizontal		+= Input.GetAxis("Horizontal");
		jumpPressed		= jumpPressed || Input.GetButtonDown("Jump");
		jumpHeld		= jumpHeld || Input.GetButton("Jump");
	}

	void ProcessTouchInputs()
	{
		if (!Application.isMobilePlatform && !testTouchControlsInEditor)
			return;

		Vector2 thumbstickInput = thumbstick.GetDirection();
		horizontal		+= thumbstickInput.x;
		jumpPressed		= jumpPressed || jumpButton.GetButtonDown();
		jumpHeld		= jumpHeld || jumpButton.GetButton();
		
	}
}
