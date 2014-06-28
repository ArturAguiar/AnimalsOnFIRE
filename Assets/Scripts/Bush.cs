using UnityEngine;
using System.Collections;

public class Bush : MonoBehaviour {

	public bool onFire = false;

	private GameManager gameManager;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
	
		spriteRenderer = this.GetComponent<SpriteRenderer>();

		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		gameManager.AddBush (this);

	}
	
	// Update is called once per frame
	void Update () {
	
		if (onFire) 
		{
			spriteRenderer.color = Color.red;
		}
		else
		{
			spriteRenderer.color = Color.white;
		}

		this.transform.position = new Vector3(this.transform.position.x - gameManager.runSpeed * Time.deltaTime,
		                                      this.transform.position.y,
		                                      this.transform.position.z);
	}

	void OnTrigger(Collider c)
	{
		Animal collider = (Animal)collider.GetComponent<Animal> ();
		if (collider != null) 
		{
			if (collider.onFire) {
				this.onFire = true;
			}
		}
	}
}
