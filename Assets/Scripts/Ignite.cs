using UnityEngine;
using System.Collections;

public class Ignite : MonoBehaviour 
{
	private Flammable parent;

	// Use this for initialization
	void Start () 
	{
		parent = this.transform.parent.GetComponent<Flammable>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (parent != null && parent.onFire)
        {
			Debug.Log("triggered!");
			Flammable thing = other.GetComponent<Flammable>();
			if (thing != null)
			{
				Debug.Log("burn!");
				thing.CatchFire();
			}
        }

    }
}
