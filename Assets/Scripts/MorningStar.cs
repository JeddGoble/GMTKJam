using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorningStar : MonoBehaviour {

    public int flailStrength = 1;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            BaseEnemy enemy = (BaseEnemy) coll.gameObject.GetComponent("BaseEnemy");
            if (enemy != null)
            {
                enemy.takeDamage(flailStrength);
            }
        }
    }
}
