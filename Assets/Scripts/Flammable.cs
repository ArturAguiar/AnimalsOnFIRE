using UnityEngine;
using System.Collections;

public class Flammable : MonoBehaviour 
{	
	public bool onFire = false;
	private ParticleEmitter innerFire;
	private ParticleEmitter outerFire;
	private Animator animator;
	private Light fireLight;
	private AudioSource scream;

	protected bool startled = false;
	protected Vector2 danger;

	// Use this for initialization
	protected virtual void Start () 
	{
		animator = this.GetComponent<Animator>();
		innerFire = this.transform.Find("Fire/InnerCore").GetComponent<ParticleEmitter>();
		outerFire = this.transform.Find("Fire/OuterCore").GetComponent<ParticleEmitter>();
		fireLight = this.transform.Find ("Fire/Lightsource").GetComponent<Light>();
		scream = this.GetComponentInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Startle(float x, float z)
	{
		startled = true;
		danger = new Vector2(x, z);
		animator.Play("Startled");
	}
	
	public void CatchFire()
	{
		if (!onFire && scream != null)
			scream.Play();

		if (animator)
			animator.Play("Burning");

		innerFire.emit = true;
		outerFire.emit = true;
		fireLight.range = 1.2f;
		onFire = true;
	}
}
