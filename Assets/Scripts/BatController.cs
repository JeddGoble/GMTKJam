using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{

	public Transform PlayerTarget;
	public float MoveSpeed = 30f;

	private Rigidbody2D rigidBody;
	private Vector2 heading;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{




	}

	void FixedUpdate()
	{
		if (PlayerTarget != null)
		{
			var targetPos = new Vector2(PlayerTarget.position.x, PlayerTarget.position.y);
			heading = (PlayerTarget.position - transform.position).normalized;
			rigidBody.velocity = heading * MoveSpeed * Time.deltaTime;
		}
	}
}
