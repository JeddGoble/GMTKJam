using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpikesController : MonoBehaviour
{

	public Sprite spikesDownSprite;
	public Sprite spikesUpSprite;

	public float Delay = 1f;
	public float SpikesUpTime = 0.75f;

	private bool spikesAreUp = false;

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
		if (other.gameObject.tag == "Player")
		{
			if (spikesAreUp)
			{
				var myPos = new Vector2(transform.position.x, transform.position.y);
				GameManager.instance.PlayerTakeDamage(1, myPos);
			}
			else
			{
                StartCoroutine(spikesUpDelayed());
			}
		}
	}

	private IEnumerator spikesUpDelayed()
	{
		yield return new WaitForSeconds(Delay);
		spriteRenderer.sprite = spikesUpSprite;

		spikesAreUp = true;

		StartCoroutine(spikesDownDelayed());
	}

	private IEnumerator spikesDownDelayed()
	{
		yield return new WaitForSeconds(SpikesUpTime);
		spriteRenderer.sprite = spikesDownSprite;

		spikesAreUp = false;
	}

}
