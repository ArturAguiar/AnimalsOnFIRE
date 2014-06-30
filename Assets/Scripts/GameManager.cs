using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	List<Animal> animals = new List<Animal>();
	List<Bush> bushes = new List<Bush> ();

    public GameObject flame;
    List<GameObject> flames = new List<GameObject>();

	public float scrollSpeed;

    public int numOnFire = 0;

    public int score;

	// Use this for initialization
	void Start () {
		Animal firstAnimal = GameObject.FindWithTag("Player").GetComponent<Animal>();
		firstAnimal.CatchFire();

        score = 0;
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

        Debug.Log(flames);
        Debug.Log(numOnFire);
        while (flames.Count < numOnFire)
        {
            GameObject addFlame = (GameObject)Instantiate(flame, new Vector3(-3 + Random.Range(-0.5f, 0.5f), 0, Random.Range(-3, 3)), new Quaternion());

            ParticleEmitter[] emitters = addFlame.GetComponentsInChildren<ParticleEmitter>();
            foreach (ParticleEmitter pe in emitters)
            {
                pe.emit = true;
            }

            flames.Add(addFlame);
        }

        while (flames.Count > numOnFire)
        {
            GameObject removeFlame = flames[0];
            flames.Remove(removeFlame);
            Destroy(removeFlame);
        }
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

    public void IncrementScore(int increment)
    {
        score += increment;
    }
}
