using UnityEngine;
using System.Collections;

public class Flickering : MonoBehaviour {

	public float minIntensity = 0.4f;
	public float maxIntensity = 0.9f;
	private Light light;

	// Use this for initialization
	void Start () {
		light = this.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {

		light.intensity = Random.Range(minIntensity, maxIntensity);

	}
}
