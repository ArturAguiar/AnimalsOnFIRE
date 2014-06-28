using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour 
{

	public float runSpeed;

	public bool onFire = false;
	public float speedX = 3.5f;
	public float speedZ = 5.0f;
	public float boundaryUp = 2.15f;
	public float boundaryDown = -2.15f;

	private Vector3 velocity;
	private SpriteRenderer spriteRenderer;

	private GameManager gameManager;

	// Use this for initialization
	void Start () 
	{
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
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
		
		velocity.y = 0.0f;
		
		this.transform.position = new Vector3(this.transform.position.x + velocity.x,
				                              this.transform.position.y + velocity.y,
				                              this.transform.position.z + velocity.z);
		
		if (this.transform.position.z > boundaryUp)
		{
			this.transform.position = new Vector3(this.transform.position.x,
			                                      this.transform.position.y,
			                                      boundaryUp);
		}
		else if (this.transform.position.z < boundaryDown)
		{
			this.transform.position = new Vector3(this.transform.position.x,
			                                      this.transform.position.y,
			                                      boundaryDown);
		}

	}
}
