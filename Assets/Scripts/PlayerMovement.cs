using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 600f;                          // Amount of force added when the player jumps.
	[SerializeField] private float m_SideJumpForce = 1000f;                          // Amount of force added when the player jumps.
	[Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;           // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .15f;   // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_LeftCheck;                             // A position marking where to check if the player is touching a wall on the left.
	[SerializeField] private Transform m_RightCheck;                            // A position marking where to check if the player is touching a wall on the right.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	const float k_GroundedRadius = 0.2f; // Radius of the overlap circle to determine if grounded
	const float k_CeilingRadius = 0.2f; // Radius of the overlap circle to determine if the player can stand up
	private bool m_Grounded;            // Whether or not the player is grounded.
	private bool m_SlideLeft = false;            // Whether or not the player is sliding on the left.
	private bool m_SlideRight = false;            // Whether or not the player is sliding on the right.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		m_SlideLeft = false;
		m_SlideRight = false;


		Collider2D[] collidersWithGround = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < collidersWithGround.Length; i++)
		{
			if (collidersWithGround[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
                {
					OnLandEvent.Invoke();
				}
			}
		}

		Collider2D[] collidersWithLeft = Physics2D.OverlapCircleAll(m_LeftCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < collidersWithLeft.Length; i++)
		{
			if (!m_Grounded && !m_SlideLeft && collidersWithLeft[i].gameObject != gameObject)
			{
				m_SlideLeft = true;
			}
		}


		Collider2D[] collidersWithRight = Physics2D.OverlapCircleAll(m_RightCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < collidersWithRight.Length; i++)
		{
			if (!m_Grounded && !m_SlideRight && collidersWithRight[i].gameObject != gameObject)
			{
				m_SlideRight = true;
			}
		}
		
	}


	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		//if (!crouch)
		//{
		//	// If the character has a ceiling preventing them from standing up, keep them crouching
		//	if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
		//	{
		//		crouch = true;
		//	}
		//}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}

		// If the player should jump...
		if (jump)
        {
			if (m_Grounded)
			{
				// Add a vertical force to the player.
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			} else if (m_SlideLeft)
			{
				// Add a vertical force to the player.
				m_SlideLeft = false;
				m_Rigidbody2D.AddForce(new Vector2(m_SideJumpForce, m_JumpForce * 0.90f));
			} else if (m_SlideRight)
			{
				// Add a vertical force to the player.
				m_SlideRight = false;
				m_Rigidbody2D.AddForce(new Vector2(-m_SideJumpForce, m_JumpForce * 0.90f));
			}
        }
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		gameObject.GetComponent<SpriteRenderer>().flipX = !m_FacingRight;
		//Vector3 theScale = transform.localScale;
		//theScale.x *= -1;
		//transform.localScale = theScale;
	}
}
