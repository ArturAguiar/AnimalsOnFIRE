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
            Animal a = other.GetComponent<Animal>();
            if (a != null)
            {
                a.onFire = true;
            }

            Bush b = other.GetComponent<Bush>();
            if (b != null)
            {
                b.onFire = true;
            }
        }

    }
}
