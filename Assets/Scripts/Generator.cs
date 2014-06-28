using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public Animal squirrel;
	public Bush bush;

	private const int SQUIRREL_INTERVAL = 200;
	private int squirrel_wait = 0;

	private const int BUSH_INTERVAL = 60;
	private int bush_wait = 0;
	

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		squirrel_wait++;
		if (squirrel_wait > SQUIRREL_INTERVAL) 
		{
			squirrel_wait = 0;
			Animal newsquirrel = (Animal)Instantiate (squirrel, new Vector3 (3, 0, 5*Random.value - 2.5f), new Quaternion ());
			newsquirrel.onFire = false;
		}

		bush_wait++;
		if (bush_wait > BUSH_INTERVAL) 
		{
			bush_wait = 0;
			Bush newbush = (Bush)Instantiate (bush, new Vector3 (3, 0, 5*Random.value - 2.5f), new Quaternion ());
			newbush.onFire = false;
		}


	}
}
