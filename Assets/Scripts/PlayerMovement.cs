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
		public float speedSmoothing = 0.2f;
		public float stopSmoothing = 0.05f;
		public float fireCooldown = 0.2f;
		public GameObject projectile;
		public float projectileSpeed = 1f;
		public GameObject cameraObj;

		private bool isFacing = true;
		private bool isMoving = false;

		const float jumpInputTime = 0.3f;
		const float groundedInputTime = 0.3f;
		private float jumpInputLast = 0f;
		private float groundedInputLast = 0f;
		private float jumpTimeLast = 0f;
		private float fireTimeLast = 0f;

		private int inputLevel = 0;
		private float inputMove = 0f;
		private bool inputJump = false;
		private bool inputFire = false;

		public UnityEvent JumpStarted;
		public UnityEvent JumpEnded;
		public UnityEvent MoveStarted;
		public UnityEvent MoveEnded;
		public UnityEvent ThrowStarted;


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

		public void Die()
        {
			InputManager.BlockKeys(1);
			Time.timeScale = 0;
			cameraObj.GetComponent<AudioSource>().Stop();

        }

		public void Start()
		{
			if (JumpStarted == null) JumpStarted = new UnityEvent();
			if (JumpEnded == null) JumpEnded = new UnityEvent();
			if (MoveStarted == null) MoveStarted = new UnityEvent();
			if (MoveEnded == null) MoveEnded = new UnityEvent();
			if (ThrowStarted == null) ThrowStarted = new UnityEvent();
		}

		public void Update()
		{
			inputMove = InputManager.GetHorzAxis(inputLevel);
			inputJump = InputManager.GetKey(KeyCode.Space, inputLevel);
			inputFire = InputManager.GetKey(KeyCode.LeftShift, inputLevel);
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

			// Speeding up
			if (((newMove >= -threshold && currMove >= -threshold) || (newMove <= threshold && currMove <= threshold)) && Mathf.Abs(newMove) >= Mathf.Abs(currMove))
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

			#endregion

			#region Fire
			fireTimeLast -= Time.deltaTime;
			if (inputFire && fireTimeLast <= 0)
            {
				fireTimeLast = fireCooldown;
				var inst = Instantiate(projectile);
				inst.transform.position = transform.position - new Vector3(0, 0.25f);
				inst.GetComponent<Rigidbody2D>().velocity = new Vector2(isFacing ? projectileSpeed : -projectileSpeed, 0);
				ThrowStarted.Invoke();
            }
            #endregion
        }

    }
}
