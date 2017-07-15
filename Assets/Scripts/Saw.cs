using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	private void Update()
	{

	}

	void FixedUpdate()
	{
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			var myPos = new Vector2(transform.position.x, transform.position.y);
			GameManager.instance.PlayerTakeDamage(1, myPos);
		}
	}
}
