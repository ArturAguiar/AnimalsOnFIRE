using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public Animal squirrel;

	private const int SQUIRREL_INTERVAL = 100;
	private int squirrel_wait = 0;
	

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		squirrel_wait++;
		if (squirrel_wait > 100) 
		{
			squirrel_wait = 0;
			Animal newsquirrel = (Animal)Instantiate (squirrel, new Vector3 (3, 0, 5*Random.value - 2.5f), new Quaternion ());
			newsquirrel.onFire = false;
		}


	}
}
