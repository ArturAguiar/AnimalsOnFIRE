using UnityEngine;
using System.Collections;

public class Deer : Animal {

	// Use this for initialization
	protected override void Start () {
	
        base.Start();

        Debug.Log("extended!");

        //initJumpSpeed = 1.0f;
	    initHealth = 500.0f;
	}
	
}
