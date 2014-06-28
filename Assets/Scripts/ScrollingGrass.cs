using UnityEngine;
using System.Collections;

public class ScrollingGrass : MonoBehaviour {

	private Vector2 offset;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		offset = Vector2.zero;

		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		offset.x -= gameManager.scrollSpeed * Time.deltaTime;
		if (offset.x < -1f)
			offset.x += 1f;

		this.renderer.materials[0].mainTextureOffset = offset;
	}
}
