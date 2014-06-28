using UnityEngine;
using System.Collections;

public class Alert : MonoBehaviour {

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
                a.Startle(this.transform.position.x, this.transform.position.z);
            }
        }
    }
}
