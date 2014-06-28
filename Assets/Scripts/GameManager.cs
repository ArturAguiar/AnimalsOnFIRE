﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	List<Animal> animals = new List<Animal>();

	// Use this for initialization
	void Start () {
		Animal firstAnimal = GameObject.FindWithTag("Player").GetComponent<Animal>();
		firstAnimal.CatchFire();
	}
	
	// Update is called once per frame
	void Update () {
	
		List<Animal> newanimals = new List<Animal> ();

		//Debug.Log (animals.Count);
		foreach (Animal a in animals) 
		{
			if (a.transform.position.x < -5) {
				Destroy(a.gameObject);
			}
			else
			{
				newanimals.Add (a);
			}
			animals = newanimals;
		}
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
}
