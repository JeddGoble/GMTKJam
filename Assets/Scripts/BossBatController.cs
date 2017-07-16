using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBatController : BaseEnemy
{
    public Transform PlayerTarget;
    public float MoveSpeed = 30f;

    private Rigidbody2D rigidBody;
    private Vector2 heading;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	override public void HandleCollisions(Collision2D coll)
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
