using UnityEngine;
using System.Collections;

public class ScrollingGrass : MonoBehaviour {

	Vector2 offset;

	// Use this for initialization
	void Start () {
		offset = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		this.renderer.materials [0].mainTextureOffset = offset;
		offset.x -= 0.05f;
		if (offset.x < -1f)
			offset.x += 1f;
	}
}
