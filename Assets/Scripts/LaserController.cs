using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(sprite != null && !sprite.enabled)
        {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            var myPos = new Vector2(transform.position.x, transform.position.y);
            GameManager.instance.PlayerTakeDamage(1, myPos);
            GameManager.instance.PlayerCharacter.resetFlail();
        }
    }
}
