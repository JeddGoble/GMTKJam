using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawHolder : MonoBehaviour {

    private SliderJoint2D sj;

    public float slideSpeed = 2;

    // Use this for initialization
    void Start () {
        sj = GetComponent<SliderJoint2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (sj.limitState == JointLimitState2D.UpperLimit)
        {
            JointMotor2D motor = new JointMotor2D();
            motor.motorSpeed = -slideSpeed;
            motor.maxMotorTorque = 10000;
            sj.motor = motor;
        }
        else if (sj.limitState == JointLimitState2D.LowerLimit)
        {
            JointMotor2D motor = new JointMotor2D();
            motor.motorSpeed = slideSpeed;
            motor.maxMotorTorque = 10000;
            sj.motor = motor;
        }
    }
}
