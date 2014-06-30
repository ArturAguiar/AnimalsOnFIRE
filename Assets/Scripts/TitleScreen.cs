using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

    public Texture backgroundTexture;
	public Texture instructionsTexture;
	private bool clickedOnce = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("return"))
        {
			if (clickedOnce)
            	Application.LoadLevel(1);
			else
				clickedOnce = true;
        }
	}

    void OnGUI()
    {
		if (clickedOnce)
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), instructionsTexture);
		else
        	GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
    }
}
