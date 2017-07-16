using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    public int hp = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            var myPos = new Vector2(transform.position.x, transform.position.y);
            GameManager.instance.PlayerTakeDamage(1, myPos);
            GameManager.instance.PlayerCharacter.resetFlail();
        }
    }

    public bool takeDamage(int amount)
    {
        hp -= amount;

        if(hp < 0)
        {
            kill();
            //returns true for dead
            return true;
        }

        return false;
    }

    public void kill()
    {
        Destroy(this.gameObject);
    }
}
