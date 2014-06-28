using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour 
{
	public bool onFire = false;
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
	private ParticleEmitter innerFire;
	private ParticleEmitter outerFire;

    private bool startled = false;
    private Vector2 danger;

    private Ignite fireSensor;
    private Alert alertSensor;

	private GameManager gameManager;

	private float health;

	// Use this for initialization
	protected virtual void Start () 
	{
		health = initHealth;

		body = this.GetComponent<Rigidbody>();
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		spriteRenderer = this.GetComponent<SpriteRenderer>();

		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		gameManager.AddAnimal (this);

		innerFire = this.transform.Find("Fire/InnerCore").GetComponent<ParticleEmitter>();
		outerFire = this.transform.Find("Fire/OuterCore").GetComponent<ParticleEmitter>();

        fireSensor = this.GetComponentInChildren<Ignite>();
        alertSensor = this.GetComponentInChildren<Alert>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!onFire)
		{
			// AI here?
            velocity.x = -gameManager.runSpeed * Time.deltaTime;
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
		spriteRenderer.color = Color.Lerp (Color.white, Color.red, (initHealth - health) / initHealth);

		if (health <= 0.0f)
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

		if (onGround && Input.GetKey("space"))
		{
			body.velocity = new Vector3(0.0f, initJumpSpeed, 0.0f);
			onGround = false;
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
	}

	void OnCollisionExit(Collision collisionInfo)
	{
		onGround = !collisionInfo.gameObject.CompareTag("Ground");
	}

	public void CatchFire()
	{
		innerFire.emit = true;
		outerFire.emit = true;
		onFire = true;

        fireSensor.onFire = true;
        alertSensor.onFire = true;
	}

    public void Startle(float x, float z)
    {
        startled = true;
        danger = new Vector2(x, z);
    }

	public void Die()
	{
		gameManager.RemoveAnimal(this);
	}
}
