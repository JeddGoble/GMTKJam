using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour {

	public Sprite FullHeartSprite;
	public Sprite EmptyHeartSprite;

	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void fillHeart()
	{
		spriteRenderer.sprite = FullHeartSprite;
	}

	public void emptyHeart()
	{
		spriteRenderer.sprite = EmptyHeartSprite;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
