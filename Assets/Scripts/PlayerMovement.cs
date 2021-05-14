using UnityEngine;
using UnityEngine.Events;
using Assets.Managers;

namespace Assets.Scripts
{
	public class PlayerMovement : MonoBehaviour
	{
		public LayerMask groundMask;
		public float moveSpeed = 400f;
		public float jumpForce = 400f;
		public float jumpCut = 0.8f;
		public float jumpControlTime = 0.6f;
		public float dashForce = 400f;
		public float dashCooldown = 1f;
		public float dashControlTime = 1f;
		public float dashSmoothing = 0.6f;
		public float speedSmoothing = 0.2f;
		public float stopSmoothing = 0.05f;

		private bool isFacing = true;
		private bool isMoving = false;

		const float jumpInputTime = 0.3f;
		const float groundedInputTime = 0.3f;
		private float jumpInputLast = 0f;
		private float groundedInputLast = 0f;
		private float jumpTimeLast = 0f;
		private float dashTimeLast = 0f;

		private int inputLevel = 0;
		private float inputMove = 0f;
		private bool inputJump = false;
		private bool inputDash = false;

		public UnityEvent JumpStarted;
		public UnityEvent JumpEnded;
		public UnityEvent DashStarted;
		public UnityEvent DashEnded;
		public UnityEvent MoveStarted;
		public UnityEvent MoveEnded;


		private Rigidbody2D RigidBody() { return GetComponent<Rigidbody2D>(); }
		private Collider2D Collider() { return GetComponent<Collider2D>(); }

		private bool IsGrounded()
		{
			return Physics2D.BoxCast(Collider().bounds.center, new Vector2(Collider().bounds.extents.x, 0.1f), 0, Vector2.down, Collider().bounds.extents.y, groundMask);
		}

		private void Flip()
		{
			// Switch the way the player is labelled as facing.
			isFacing = !isFacing;

			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

		public void Start()
		{
			if (JumpStarted == null) JumpStarted = new UnityEvent();
			if (JumpEnded == null) JumpEnded = new UnityEvent();
			if (DashStarted == null) DashStarted = new UnityEvent();
			if (DashEnded == null) DashEnded = new UnityEvent();
			if (MoveStarted == null) MoveStarted = new UnityEvent();
			if (MoveEnded == null) MoveEnded = new UnityEvent();
		}

		public void Update()
		{
			inputMove = InputManager.GetHorzAxis(inputLevel);
			inputJump = InputManager.GetKey(KeyCode.Space, inputLevel);
			inputDash = InputManager.GetKey(KeyCode.LeftShift, inputLevel);
		}

		public void FixedUpdate()
		{

			#region Grounded Update
			groundedInputLast -= Time.deltaTime;
			if (IsGrounded())
			{
				groundedInputLast = groundedInputTime;
			}
			#endregion

			#region Jump Cutting

			// If Jump Ended 
			if (jumpTimeLast <= jumpControlTime && (jumpTimeLast + Time.deltaTime) > jumpControlTime)
			{
				if (RigidBody().velocity.y > 0)
					RigidBody().velocity = new Vector2(RigidBody().velocity.x, RigidBody().velocity.y * jumpCut);
				JumpEnded.Invoke();
			}

			if (jumpTimeLast <= jumpControlTime)
			{
				jumpTimeLast += Time.deltaTime;

				if (!inputJump && jumpTimeLast < jumpControlTime)
					jumpTimeLast = jumpControlTime;

			}
			#endregion

			#region Jump Debouncing
			jumpInputLast -= Time.deltaTime;
			if (inputJump)
			{
				jumpInputLast = jumpInputTime;
			}

			bool jump = inputJump && (groundedInputLast > 0) && (jumpInputLast > 0);
			if (jump)
			{
				groundedInputLast = 0;
				jumpInputLast = 0;
			}
			#endregion

			#region Flip Facing
			// If the input is moving the player right and the player is facing left...
			if (inputMove > 0 && !isFacing)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (inputMove < 0 && isFacing)
			{
				// ... flip the player.
				Flip();
			}
			#endregion

			#region Ground Movement
			Vector3 v = new Vector3();
			float newMove = inputMove * moveSpeed * Time.deltaTime;
			float currMove = RigidBody().velocity.x;
			float threshold = 0.1f;

			// Dashing
			if (dashTimeLast <= dashControlTime)
			{
				Vector3 targetVelocity = new Vector2(0, 0);
				RigidBody().velocity = Vector3.SmoothDamp(RigidBody().velocity, targetVelocity, ref v, dashSmoothing);
			}
			// Speeding up
			else if (((newMove >= -threshold && currMove >= -threshold) || (newMove <= threshold && currMove <= threshold)) && Mathf.Abs(newMove) >= Mathf.Abs(currMove))
			{
				Vector3 targetVelocity = new Vector2(newMove, RigidBody().velocity.y);
				RigidBody().velocity = Vector3.SmoothDamp(RigidBody().velocity, targetVelocity, ref v, speedSmoothing);
			}
			// Stopping 
			else
			{
				Vector3 targetVelocity = new Vector2(0, RigidBody().velocity.y);
				RigidBody().velocity = Vector3.SmoothDamp(RigidBody().velocity, targetVelocity, ref v, stopSmoothing);
			}

			if (isMoving && Mathf.Abs(v.x) <= threshold)
			{
				isMoving = false;
				MoveEnded.Invoke();
			}
			else if (!isMoving && Mathf.Abs(v.x) > threshold)
			{
				isMoving = true;
				MoveStarted.Invoke();
			}

			#endregion

			#region Action Movement
			// If the player should jump...
			if (jump)
			{
				// Add a vertical force to the player.
				RigidBody().velocity = new Vector2(RigidBody().velocity.x, jumpForce);
				jumpTimeLast = 0;
				JumpStarted.Invoke();
			}

			// If the dash has ended
			if (dashTimeLast <= dashControlTime && (dashTimeLast + Time.deltaTime) > dashControlTime)
			{
				RigidBody().velocity = new Vector2(0, 0);
				DashEnded.Invoke();
			}

			// If the player should dash
			if (inputDash && dashTimeLast > dashCooldown)
			{
				float force = dashForce * (isFacing ? 1 : -1);
				RigidBody().AddForce(new Vector2(force, 0f));
				if (RigidBody().velocity.y <= threshold)
				{
					RigidBody().velocity = new Vector2(RigidBody().velocity.x, 0f);
				}
				dashTimeLast = 0;
				DashStarted.Invoke();
			}

			if (dashTimeLast <= dashCooldown)
				dashTimeLast += Time.deltaTime;

			#endregion
		}

	}
}
