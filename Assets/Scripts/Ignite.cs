using UnityEngine;
using System.Collections;

public class Ignite : MonoBehaviour {

    public bool onFire = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (onFire)
        {
			Flammable thing = other.GetComponent<Flammable>();
			if (thing != null)
			{
				thing.CatchFire();
			}
        }

    }
}
