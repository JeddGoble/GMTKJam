using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpikeController : MonoBehaviour
{

	public Sprite spikesUpSprite;
	public Sprite spikesDownSprite;
	public float delay;

	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.tag == "Player")
		{
			spriteRenderer.sprite = spikesUpSprite;

		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.tag == "Player")
		{
			spriteRenderer.sprite = spikesDownSprite;
		}
	}
}
