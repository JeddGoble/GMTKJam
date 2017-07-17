using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {

    public int hp = 1;

    public SFXController sfx = null;

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

		HandleCollisions(coll);
    }

	public abstract void HandleCollisions(Collision2D coll);

    public bool takeDamage(int amount)
    {
        hp -= amount;

        if(hp < 0)
        {
            kill();
            //returns true for dead
            return true;
        }
        sfx.playEnemyHit();

        return false;
    }

    public void kill()
    {
        if(this is GoblinBossController || this is BossBatController)
        {
            sfx.playBossKill();
        }
        else
        {
            sfx.playEnemyKill();
        }
        Destroy(this.gameObject);
    }
}
