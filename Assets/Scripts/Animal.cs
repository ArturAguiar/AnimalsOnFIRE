using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour 
{
	public bool onFire = false;
	public float speedX = 3.5f;
	public float speedZ = 5.0f;
	public float initJumpSpeed = 1.0f;
	public float boundaryUp = 2.15f;
	public float boundaryDown = -2.15f;

	private Vector3 velocity;
	private bool onGround = true;
	private Rigidbody body;
	private SpriteRenderer spriteRenderer;

	private GameManager gameManager;

	// Use this for initialization
	void Start () 
	{
		body = this.GetComponent<Rigidbody>();
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		spriteRenderer = this.GetComponent<SpriteRenderer>();

		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		gameManager.AddAnimal (this);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!onFire)
		{
			// AI here?
			spriteRenderer.color = Color.white;
			this.transform.position = new Vector3(this.transform.position.x - gameManager.runSpeed * Time.deltaTime,
			                                      this.transform.position.y,
			                                      this.transform.position.z);

			return;
		}

		spriteRenderer.color = Color.red;

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

		if (onGround && Input.GetKey("space"))
		{
			body.velocity = new Vector3(0.0f, initJumpSpeed, 0.0f);
		}

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

	void OnCollisionEnter(Collision collisionInfo)
	{
		onGround = collisionInfo.gameObject.CompareTag("Ground");

		Animal collider = (Animal)collisionInfo.collider.GetComponent<Animal> ();
		if (collider != null) 
		{
			if (collider.onFire) {
				this.onFire = true;
			}
		}
	}

	void OnCollisionExit(Collision collisionInfo)
	{
		onGround = !collisionInfo.gameObject.CompareTag("Ground");
	}
}
