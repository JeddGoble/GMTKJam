using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{

	public float speed = 14f;
	private Vector2 input;
	private Vector2 rStick;
	private SpriteRenderer sr;
	private Rigidbody2D rb;
	private Animator animator;

	private float width;
	private float height;

	public float TakeDamageKickback;
	public float InvicibleTime;
	private float invincibleTimer;
	private bool isInvincible = false;
	private bool isAnimatingKickback = false;
	public float KickbackTime;
	private float kickbackTimer;

	public bool IsInvincible()
	{
		return isInvincible;
	}

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();

		width = GetComponent<BoxCollider2D>().bounds.extents.x + 0.1f;
		height = GetComponent<BoxCollider2D>().bounds.extents.y + 0.2f;

		invincibleTimer = InvicibleTime;
		kickbackTimer = KickbackTime;
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		input.x = Input.GetAxis("Horizontal");
		input.y = Input.GetAxis("Vertical");

		if (input.x < 0f)
		{
			sr.flipX = true;
		}
		else if (input.x > 0f)
		{
			sr.flipX = false;
		}

		if (isInvincible)
		{
			invincibleTimer -= Time.deltaTime;
			if (invincibleTimer <= 0f)
			{
				animator.SetBool("IsInvincible", false);
				isInvincible = false;
				invincibleTimer = InvicibleTime;
			}
		}
	}

	void FixedUpdate()
	{
		if (isAnimatingKickback)
		{
			kickbackTimer -= Time.deltaTime;

			if (kickbackTimer <= 0)
			{
				kickbackTimer = KickbackTime;
				isAnimatingKickback = false;
			}
		}
		else
		{
			rb.velocity = new Vector2(input.x * speed, input.y * speed);
		}

	}

	public void TakeDamage(Vector2 fromDirection)
	{


		if (!isInvincible)
		{
			print("Should show damage animation");

			animator.SetBool("IsInvincible", true);
			isInvincible = true;
			rb.AddForce(fromDirection * TakeDamageKickback, ForceMode2D.Impulse);
			isAnimatingKickback = true;
		}
	}
}
