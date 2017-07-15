using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpikesController : MonoBehaviour {

	public Sprite spikesDownSprite;
	public Sprite spikesUpSprite;

	public float Delay;

	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			spriteRenderer.sprite = spikesUpSprite;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			spriteRenderer.sprite = spikesDownSprite;
		}
	}


}
