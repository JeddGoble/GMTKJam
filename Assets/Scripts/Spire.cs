using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spire : MonoBehaviour {
    public HingeJoint2D laser;

    public float timer = 0;

    public float deathTimer = 0;

    public float interval = 6;

    public float deathInterval = 1;

    private bool active = false;

    public float sweepSpeed = 50;

	// Use this for initialization
	void Start () {
        laser.GetComponent<SpriteRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (!active) {
            timer += Time.deltaTime;
            if (timer > interval)
            {
                active = true;
                laser.GetComponent<SpriteRenderer>().enabled = true;

                JointMotor2D motor = new JointMotor2D();
                motor.motorSpeed = 50;
                motor.maxMotorTorque = 10000;

                HingeJoint2D hinge = laser.GetComponent<HingeJoint2D>();
                hinge.motor = motor;
                hinge.useMotor = true;

                timer = 0;
            }
        } else if(active)
        {
            deathTimer += Time.deltaTime;
            if(deathTimer > deathInterval)
            {
                laser.GetComponent<SpriteRenderer>().enabled = false;
                active = false;
                HingeJoint2D hinge = laser.GetComponent<HingeJoint2D>();
                JointMotor2D motor = new JointMotor2D();
                motor.motorSpeed = Random.Range(100, 1000);
                motor.maxMotorTorque = 10000;
                hinge.useMotor = true;
                hinge.motor = motor;
                deathTimer = 0;
            }
        }

        /*if(!ready)
        {
            GameObject hero = GameObject.Find("Hero");
            float dx = hero.transform.position.x - laser.anchor.x;
            float dy = hero.transform.position.y - laser.anchor.y;
            float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
            float laserAngle = laser.jointAngle % 360;

            float distance = angle - laserAngle;
            Debug.Log(angle + ":" + laserAngle + ":" + distance);
        }*/
    }


}
