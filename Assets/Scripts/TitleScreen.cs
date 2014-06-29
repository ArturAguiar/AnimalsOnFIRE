using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

    public Texture backgroundTexture;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("return"))
        {
            Application.LoadLevel(1);
        }
	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
    }
}
