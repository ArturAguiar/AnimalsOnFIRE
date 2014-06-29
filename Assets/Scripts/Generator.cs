using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {


    public Bomb bomb;

	public Flammable squirrel;
	public Flammable deer;
	public Flammable bush;


	public float squirrelFreq = 1.0f;
	public float bushFreq = 2.0f;
	public float deerFreq = 0.5f;

    private const int BOMB_INTERVAL = 80;
    private int bomb_wait = 50;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Random.Range(0.0f, 100.0f) <= squirrelFreq)
			Instantiate (squirrel, new Vector3 (10, 1, 5 * Random.value - 2.5f), new Quaternion ());
		
		if (Random.Range(0.0f, 100.0f) <= deerFreq)
			Instantiate (deer, new Vector3 (10, 1, 5 * Random.value - 2.5f), new Quaternion ());
		
		if (Random.Range(0.0f, 100.0f) <= bushFreq)
			Instantiate (bush, new Vector3 (10, 1, 5 * Random.value - 2.5f), new Quaternion ());


        bomb_wait++;
        if (bomb_wait > BOMB_INTERVAL)
        {
            bomb_wait = 0;
            Bomb newbomb = (Bomb)Instantiate(bomb, new Vector3(10, 1, 5 * Random.value - 2.5f), new Quaternion());
            newbomb.onFire = false;
        }

	}
}
