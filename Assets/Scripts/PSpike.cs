using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSpike : MonoBehaviour {

    private float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 2)
        {
            Destroy(this.gameObject);
        }
    }
}
