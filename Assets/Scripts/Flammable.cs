using UnityEngine;
using System.Collections;

public class Flammable : MonoBehaviour 
{	
	public enum State 
	{
		IDLE,
		STARTLED,
		BURNING
	}

	public State state = State.IDLE;
	public AudioSource[] startleSounds;
	public AudioSource[] igniteSounds;

	private ParticleEmitter innerFire;
	private ParticleEmitter outerFire;
	private Animator animator;
	private Light fireLight;

    protected GameManager gameManager;

	protected Vector2 danger;

	// Use this for initialization
	protected virtual void Start () 
	{
		animator = this.GetComponent<Animator>();
		innerFire = this.transform.Find("Fire/InnerCore").GetComponent<ParticleEmitter>();
		outerFire = this.transform.Find("Fire/OuterCore").GetComponent<ParticleEmitter>();
		fireLight = this.transform.Find ("Fire/Lightsource").GetComponent<Light>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public virtual void Startle(float x, float z)
	{
		if (state == State.IDLE)
		{
			state = State.STARTLED;
			danger = new Vector2(x, z);

			if (animator)
				animator.Play("Startled");

			if (startleSounds.Length > 0 && Random.Range(0, 100) < 60)
				Instantiate(startleSounds[Random.Range(0, startleSounds.Length - 1)], this.transform.position, new Quaternion());
		}
	}
	
	public virtual void CatchFire()
	{
		if (state == State.BURNING)
			return; // already on fire.

		if (igniteSounds.Length > 0 && Random.Range(0, 100) < 60)
			Instantiate(igniteSounds[Random.Range(0, igniteSounds.Length - 1)], this.transform.position, new Quaternion());
		
		gameManager.IncrementScore();                   

		innerFire.emit = true;
		outerFire.emit = true;
		fireLight.range = 1.2f;
		state = State.BURNING;

		if (animator)
		{
			animator.Play("Burning");
		}
	}
}
