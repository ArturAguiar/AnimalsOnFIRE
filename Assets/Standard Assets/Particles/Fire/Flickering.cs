using UnityEngine;
using System.Collections;

public class Flickering : MonoBehaviour {

	public float minIntensity = 0.4f;
	public float maxIntensity = 0.9f;

	private Light fireLight;

	// Use this for initialization
	void Start () {
		fireLight = this.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {

		fireLight.intensity = Random.Range(minIntensity, maxIntensity);

	}
}
