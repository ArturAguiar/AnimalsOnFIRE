using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour 
{
	public bool onFire = false;
	public float speedX = 3.5f;
	public float speedZ = 5.0f;

	private Vector3 velocity;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () 
	{
		velocity = new Vector3();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		//spriteRenderer.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!onFire)
		{
			// AI here?
			return;
		}

		if (Input.GetKey("right"))
			velocity.x = speedX * Time.deltaTime;
		else if (Input.GetKey("left"))
			velocity.x = -speedX * Time.deltaTime;
		else
			velocity.x = 0.0f;
		
		if (Input.GetKey("up"))
			velocity.z = speedZ * Time.deltaTime;
		else if (Input.GetKey("down"))
			velocity.z = -speedZ * Time.deltaTime;
		else
			velocity.z = 0.0f;

		Debug.Log(velocity);

		this.transform.position = new Vector3(this.transform.position.x + velocity.x,
				                              this.transform.position.y + velocity.y,
				                              this.transform.position.z + velocity.z);

	}
}
