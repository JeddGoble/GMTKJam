using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinArcherController : MonoBehaviour
{

	enum Intent { Idle, GetCloser, RunAway, MoveAwayFromWall, ShootArrow };

	public Transform PlayerTarget;
	public float MoveSpeed = 50f;

	public float MinRangeToPlayer = 3f;
	public float MaxRangeToPlayer = 8f;

	public float ArrowDistance = 10f;
	public float ArrowMaxFrequency = 3f;
	private float timeSinceLastArrow;

	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rigidBody;
	private Animator animator;

	private Vector2 directionToPlayer = new Vector2();
	private Vector2 currentHeading = new Vector2();
	private Vector2 directionAwayFromWall = new Vector2();

	public float ActionTime = 2f;
	public float InitialActionTime = 0f;
	private float currentActionTimer;
	private Intent currentIntent;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start()
	{
		currentActionTimer = InitialActionTime;
		timeSinceLastArrow = 0f;
	}

	// Update is called once per frame
	void Update()
	{

		currentActionTimer -= Time.deltaTime;
		timeSinceLastArrow += Time.deltaTime;

		if (currentActionTimer <= 0)
		{
			currentIntent = determineNextIntent();
			currentActionTimer = ActionTime;

		}

        updateCurrentAnimation();
		updateFacingDirection();
	}

	void FixedUpdate()
	{
		if (PlayerTarget != null)
		{
			var targetPos = new Vector2(PlayerTarget.position.x, PlayerTarget.position.y);
			directionToPlayer = (PlayerTarget.position - transform.position).normalized;

			switch (currentIntent)
			{
				case Intent.GetCloser:
					currentHeading = directionToPlayer;
					rigidBody.velocity = currentHeading * MoveSpeed * Time.deltaTime;
					break;
				case Intent.RunAway:
					currentHeading = -directionToPlayer;
					rigidBody.velocity = currentHeading * MoveSpeed * Time.deltaTime;
					break;
				case Intent.MoveAwayFromWall:
					currentHeading = directionAwayFromWall;
					rigidBody.velocity = currentHeading* MoveSpeed * Time.deltaTime;
					break;
				case Intent.ShootArrow:
					currentHeading = directionToPlayer;
					rigidBody.velocity = Vector2.zero;
					break;
				case Intent.Idle:
					break;
			}
		}
	}

	private void updateCurrentAnimation()
	{
		switch (currentIntent)
			{
				case Intent.GetCloser:
					animator.SetBool("IsWalking", true);
					animator.SetBool("IsShooting", false);
					break;
				case Intent.RunAway:
					animator.SetBool("IsWalking", true);
					animator.SetBool("IsShooting", false);
					break;
				case Intent.MoveAwayFromWall:
					animator.SetBool("IsWalking", true);
					animator.SetBool("IsShooting", false);
					break;
				case Intent.ShootArrow:
					animator.SetBool("IsWalking", false);
					animator.SetBool("IsShooting", true);
					break;
				case Intent.Idle:
					animator.SetBool("IsWalking", false);
					animator.SetBool("IsShooting", false);
					break;
			}
	}

	private Intent determineNextIntent()
	{
		float distanceToPlayer = Vector3.Distance(PlayerTarget.position, transform.position);

		if (distanceToPlayer < MinRangeToPlayer)
		{
			print("Running away");
			return Intent.RunAway;
		}
		else if (distanceToPlayer > MinRangeToPlayer && distanceToPlayer < MaxRangeToPlayer && timeSinceLastArrow > ArrowMaxFrequency)
		{
			print("Firing arrow");
			return Intent.ShootArrow;
		}
		else if (distanceToPlayer > MaxRangeToPlayer)
		{
			print("Getting closer");
			return Intent.GetCloser;
		}

		return Intent.Idle;
	}

	private void updateFacingDirection()
	{
		if (currentHeading.x < 0)
		{
			spriteRenderer.flipX = true;
		}
		else
		{
			spriteRenderer.flipX = false;
		}
	}

	public void DidCompleteShootingArrow()
	{
		timeSinceLastArrow = 0f;
		animator.SetBool("IsShooting", false);
		currentIntent = determineNextIntent();
		updateCurrentAnimation();
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Wall")
		{
			print("Goblin hit wall");

			directionAwayFromWall = (coll.gameObject.transform.position - transform.position).normalized;
			currentHeading = directionAwayFromWall;

			currentIntent = Intent.MoveAwayFromWall;
			currentActionTimer = ActionTime;
			updateCurrentAnimation();
		}
	}
}
