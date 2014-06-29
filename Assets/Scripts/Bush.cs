using UnityEngine;
using System.Collections;

public class Bush : Flammable 
{
	private GameManager gameManager;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	new void Start () 
	{
		base.Start();

		spriteRenderer = this.GetComponent<SpriteRenderer>();

		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		gameManager.AddBush (this);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (onFire)
			spriteRenderer.color = Color.gray;

		this.transform.position = new Vector3(this.transform.position.x - gameManager.scrollSpeed * Time.deltaTime,
		                                      this.transform.position.y,
		                                      this.transform.position.z);
	}
}
