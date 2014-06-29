using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public Flammable squirrel;
	public Flammable deer;
	public Flammable bush;

	public float squirrelFreq = 1.0f;
	public float bushFreq = 2.0f;
	public float deerFreq = 0.5f;

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



	}
}
