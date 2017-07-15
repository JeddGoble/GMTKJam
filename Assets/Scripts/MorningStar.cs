using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorningStar : MonoBehaviour {
    public int scale = 1;

	// Use this for initialization
	void Start () {
        transform.localScale += new Vector3(scale, scale, 1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
