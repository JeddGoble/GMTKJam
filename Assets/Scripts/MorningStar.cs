using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorningStar : MonoBehaviour {

    public int flailStrength = 1;

    private int kills = 0;

    private float flailScale = 3;

    private bool reset = false;

    public HingeJoint2D link;

<<<<<<< HEAD
    public int mass = 0;

=======
>>>>>>> Flail grows and shrings
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().mass = mass;
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        if (kills > 1)
        {
            kills = 0;
            flailScale += 1;
            transform.localScale = new Vector3(flailScale, flailScale, 1);
            float radius = GetComponent<CircleCollider2D>().radius;
            link.anchor.Set(link.anchor.x + radius, link.anchor.y + (radius * 2));
<<<<<<< HEAD
            GetComponent<Rigidbody2D>().mass += 5;
=======
>>>>>>> Flail grows and shrings
        }

        if(reset)
        {
            flailScale = 3;
            reset = false;
            transform.localScale = new Vector3(flailScale, flailScale, 1);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            BaseEnemy enemy = (BaseEnemy) coll.gameObject.GetComponent("BaseEnemy");
            if (enemy != null)
            {
                bool dead = enemy.takeDamage(flailStrength);
                if(dead)
                {
                    kills += 1;
                }
            }
        }
    }

    public void resetFlail()
    {
        reset = true;
<<<<<<< HEAD
        GetComponent<Rigidbody2D>().mass = mass;
=======
>>>>>>> Flail grows and shrings
    }
}
