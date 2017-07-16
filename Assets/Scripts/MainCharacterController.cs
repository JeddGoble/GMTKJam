using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public Rigidbody2D flail;

	public float speed = 14f;
	public float FlailRangeCoefficient = 2f;
	public float IdleAnimationSpeedThreshhold = 10f;
	private Vector2 moveInput;
	private Vector2 rightStick;
	private SpriteRenderer sr;
	private Rigidbody2D rb;
	private Animator animator;

	private float width;
	private float height;

	public GameObject FlailFirstLink;

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

		moveInput.x = Input.GetAxis("Horizontal");
		moveInput.y = Input.GetAxis("Vertical");

		bool isMoving = (rb.velocity.x > IdleAnimationSpeedThreshhold && rb.velocity.y > IdleAnimationSpeedThreshhold && Mathf.Abs(moveInput.x) > 0 && Mathf.Abs(moveInput.y) > 0);

		if ((moveInput.x != 0f || moveInput.y != 0f) && !isAnimatingKickback)
		{
			animator.SetBool("IsWalking", true);
		}
		else if (isAnimatingKickback || !isMoving)
		{
			animator.SetBool("IsWalking", false);
		}


		if (moveInput.x < 0f)
		{
			sr.flipX = true;
		}
		else if (moveInput.x > 0f)
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

		rightStick.x = Input.GetAxis("RightH");
		rightStick.y = Input.GetAxis("RightV");

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
			rb.velocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
		}
	}

	public void TakeDamage(Vector2 fromDirection)
	{
		if (!isInvincible)
		{
			animator.SetBool("IsInvincible", true);
			isInvincible = true;
			rb.AddForce(fromDirection * TakeDamageKickback, ForceMode2D.Impulse);
			isAnimatingKickback = true;
		}
	}

    public void resetFlail()
    {
        MorningStar star = (MorningStar)flail.GetComponent("MorningStar");
        star.resetFlail();
    }

    public void Kill()
    {

    }
}
