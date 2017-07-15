using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    public Rigidbody2D pSpike;
    public float frequency = 1;
    public float timer = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        Rigidbody2D spike = null;

        timer += Time.deltaTime;
        if(timer > frequency)
        {
            spike = (Rigidbody2D)Instantiate(pSpike, transform.position, transform.rotation);
            timer = 0;
        }

        if (spike != null)
        {
            spike.AddForce(new Vector3(-300, 0, 0));
        }
    }
}
