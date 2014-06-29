using UnityEngine;
using System.Collections;

public class BlastTrigger : MonoBehaviour {

    Bomb parent;

	// Use this for initialization
	void Start () {

        parent = this.GetComponentInParent<Bomb>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (parent != null)
        {
            Animal thing = other.GetComponent<Animal>();
            if (thing != null)
            {
                parent.animals.Add(thing);
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (parent != null)
        {
            Animal thing = other.GetComponent<Animal>();
            if (thing != null)
            {
                parent.animals.Remove(thing);
            }
        }
    }
}
