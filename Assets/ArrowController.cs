using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rigidBody;

	public float arrowLiftime = 2f;
	private float lifetimeTimer = 2f;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		rigidBody = GetComponent<Rigidbody2D>();
	}


	// Use this for initialization
	void Start()
	{

		lifetimeTimer = arrowLiftime;
	}

	// Update is called once per frame
	void Update()
	{

		lifetimeTimer -= Time.deltaTime;

		if (lifetimeTimer <= 0f)
		{
			print("Arrow should destroy self");
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			var myPos = new Vector2(transform.position.x, transform.position.y);
			GameManager.instance.PlayerTakeDamage(1, myPos);
			GameManager.instance.PlayerCharacter.resetFlail();

			Destroy(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
