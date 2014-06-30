using UnityEngine;
using System.Collections;

public class GameoverScreen : MonoBehaviour 
{
    public Texture backgroundTexture;

	// Use this for initialization
	void Start () {	

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("return"))
        {
            Application.LoadLevel(0);
        }
	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

		GUI.Box(new Rect(Screen.width / 2.0f - 80.0f, Screen.height / 2.0f - 15.0f, 160.0f, 30.0f), 
		        "Final score: " + GameManager.score.ToString());
    }
}
