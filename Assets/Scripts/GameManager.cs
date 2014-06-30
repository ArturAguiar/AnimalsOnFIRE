using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	List<Animal> animals = new List<Animal>();
	List<Bush> bushes = new List<Bush> ();

	public float scrollSpeed;

    public int numOnFire = 0;

    public int score;

	// Use this for initialization
	void Start () {
		Animal firstAnimal = GameObject.FindWithTag("Player").GetComponent<Animal>();
		firstAnimal.CatchFire();

        score = 0;
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
		List<Animal> newanimals = new List<Animal> ();
        numOnFire = 0;

		foreach (Animal a in animals) 
		{
			if (a.transform.position.x < -5) {
				Destroy(a.gameObject);
			}
			else
			{
				newanimals.Add (a);
                if (a.state == Flammable.State.BURNING)
                {
                    numOnFire++;
                }
			}

		}

		score += (int)(Mathf.Pow (numOnFire, 1.5f));

        if (numOnFire == 0)
        {
            Application.LoadLevel(2);
        }
		animals = newanimals;

		List<Bush> newbushes = new List<Bush> ();
		
		foreach (Bush b in bushes) 
		{
			if (b.transform.position.x < -5) {
				Destroy(b.gameObject);
			}
			else
			{
				newbushes.Add (b);
			}
		}
		bushes = newbushes;
	}


    void OnGUI()
    {
         GUI.TextArea(new Rect(10, 10, 50, 20), "" + score);
    }

	public void AddAnimal (Animal a)
	{
		animals.Add (a);
	}

	public void RemoveAnimal (Animal a)
	{
		Destroy(a.gameObject);
		animals.Remove (a);
	}

	public void AddBush (Bush b)
	{
		bushes.Add (b);
	}
}
