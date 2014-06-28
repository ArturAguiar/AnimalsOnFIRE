using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour 
{
	public bool onFire;
	public float speedX;
	public float speedZ;

	public float runSpeed;

	private Vector3 velocity;
	private SpriteRenderer spriteRenderer;

	private GameManager gameManager;

	// Use this for initialization
	void Start () 
	{
		velocity = new Vector3();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		//spriteRenderer.color = Color.red;

		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		gameManager.AddAnimal (this);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!onFire)
		{
			// AI here?

			this.transform.position = new Vector3(this.transform.position.x - runSpeed * Time.deltaTime,
			                                      this.transform.position.y,
			                                      this.transform.position.z);

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

		this.transform.position = new Vector3(this.transform.position.x + velocity.x,
				                              this.transform.position.y + velocity.y,
				                              this.transform.position.z + velocity.z);



	}
}
