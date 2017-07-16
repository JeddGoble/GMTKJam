using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsManager : MonoBehaviour {

	public HeartController[] hearts;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetHearts(int numberOfHeartsFull)
	{
		print("Filling hearts");

		// Guard against out of index
		if (numberOfHeartsFull > hearts.Length)
		{
			print("I only have so many hearts to give! Sorry. :(");
			return;
		}

		for (int i = 0; i < hearts.Length; i++)
		{
			if (i < numberOfHeartsFull)
			{
				hearts[i].fillHeart();
			}
			else
			{
				hearts[i].emptyHeart();
			}
		}

	}
}
