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

	//public bool onFire = false;
	private ParticleEmitter innerFire;
	private ParticleEmitter outerFire;
	private Animator animator;
	private Light fireLight;
	private AudioSource scream;

    protected GameManager gameManager;

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
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Startle(float x, float z)
	{
		if (state == State.IDLE)
		{
			startled = true;
			danger = new Vector2(x, z);
			animator.Play("Startled");
		}
	}
	
	public void CatchFire()
	{
		if (state == State.BURNING)
			return; // already on fire.

	    if(scream != null)
	        scream.Play();

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
