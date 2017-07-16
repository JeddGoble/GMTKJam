using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		print("Collision detected");

		if (other.gameObject.tag == "Player")
		{
			GameManager.instance.PlayerGainHeart();
			Destroy(this.gameObject);
		}
	}
}
