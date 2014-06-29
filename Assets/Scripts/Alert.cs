using UnityEngine;
using System.Collections;

public class Alert : MonoBehaviour {

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
			Flammable thing = other.GetComponent<Flammable>();
			if (thing != null)
			{
				thing.Startle(this.transform.position.x, this.transform.position.z);
			}
        }
    }
}
