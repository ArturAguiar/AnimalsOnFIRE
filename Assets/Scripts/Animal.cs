using UnityEngine;
using System.Collections;

public class Animal : Flammable
{
	public float speedX = 3.5f;
	public float speedZ = 5.0f;
    public float runawayspeed = 1.3f;
	public float initJumpSpeed = 1.0f;

	public float boundaryUp = 2.5f;
	public float boundaryDown = -2.5f;
	public float burningRate = 3.0f;
	public float initHealth = 100.0f;

	private Vector3 velocity;
	private bool onGround = true;
	private Rigidbody body;
	private SpriteRenderer spriteRenderer;

    private float perturbation = 0.025f;
    

	private GameManager gameManager;

	private float health;

	// Use this for initialization
	protected override void Start ()
	{
		base.Start();

		health = initHealth;

		body = this.GetComponent<Rigidbody>();
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		spriteRenderer = this.GetComponent<SpriteRenderer>();

		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		gameManager.AddAnimal (this);
	}
	
	// Update is called once per frame
	void Update () 
	{
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

		if (!onFire)
		{
			// AI here?
            velocity.x = -gameManager.scrollSpeed * Time.deltaTime;
            velocity.y = 0;
            velocity.z = 0;

            if (startled)
            {
                Vector2 position = new Vector2(this.transform.position.x, this.transform.position.z);
                Vector2 direction = position - danger;
                direction.Normalize();

                velocity.x += runawayspeed*Time.deltaTime*direction.x;
                velocity.z += runawayspeed * Time.deltaTime*direction.y;

            }
			spriteRenderer.color = Color.white;
			this.transform.position = new Vector3(this.transform.position.x + velocity.x,
			                                      this.transform.position.y + velocity.y,
			                                      this.transform.position.z + velocity.z);

			return;
		}

		health -= burningRate * Time.deltaTime;
		spriteRenderer.color = Color.Lerp (Color.white, Color.black, (initHealth - health) / initHealth);

		if (health <= 0.0f || this.transform.position.y < -10)
		{
			Die();
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

        velocity.x += perturbation*(Random.value - 0.5f);
        velocity.z += perturbation*(Random.value - 0.5f);

		if (onGround && Input.GetKey("space"))
		{
			body.velocity = new Vector3(0.0f, initJumpSpeed, 0.0f);
			onGround = false;
		}
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		onGround = collisionInfo.gameObject.CompareTag("Ground");
	}

	void OnCollisionExit(Collision collisionInfo)
	{
		onGround = !collisionInfo.gameObject.CompareTag("Ground");
	}

	public void Die()
	{
		gameManager.RemoveAnimal(this);
	}
}
