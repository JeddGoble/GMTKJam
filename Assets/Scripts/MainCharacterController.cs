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

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		//animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();

		width = GetComponent<BoxCollider2D>().bounds.extents.x + 0.1f;
		height = GetComponent<BoxCollider2D>().bounds.extents.y + 0.2f;
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
		} else if (input.x > 0f)
		{
			sr.flipX = false;
		}

	}

	void FixedUpdate()
	{
		rb.velocity = new Vector2(input.x* speed, input.y* speed);
	}
}
