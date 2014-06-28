using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public Animal squirrel;
    public Deer deer;
	public Bush bush;

	private const int SQUIRREL_INTERVAL = 160;
	private int squirrel_wait = 0;

	private const int BUSH_INTERVAL = 60;
	private int bush_wait = 0;

    private const int DEER_INTERVAL = 400;
    private int deer_wait = 350;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		squirrel_wait++;
		if (squirrel_wait > SQUIRREL_INTERVAL) 
		{
			squirrel_wait = 0;
			Animal newsquirrel = (Animal)Instantiate (squirrel, new Vector3 (3, 5, 5*Random.value - 2.5f), new Quaternion ());
			newsquirrel.onFire = false;
		}

		bush_wait++;
		if (bush_wait > BUSH_INTERVAL) 
		{
			bush_wait = 0;
			Bush newbush = (Bush)Instantiate (bush, new Vector3 (3, 5, 5*Random.value - 2.5f), new Quaternion ());
			newbush.onFire = false;
		}

        deer_wait++;
        if (deer_wait > DEER_INTERVAL)
        {
            deer_wait = 0;
            Deer newdeer = (Deer)Instantiate(deer, new Vector3(3, 5, 5 * Random.value - 2.5f), new Quaternion());
            newdeer.onFire = false;
        }


	}
}
