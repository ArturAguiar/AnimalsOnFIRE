using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomb : Flammable {

    private int fireLength = 0;
    private const int damage = 50;
    private const int explosionTime = 20;

    public GameObject explosion;
    public GameObject currentExplosion = null;

    public List<Animal> animals;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        animals = new List<Animal>();
        state = State.IDLE;
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position = new Vector3(this.transform.position.x - gameManager.scrollSpeed * Time.deltaTime,
                                      this.transform.position.y,
                                      this.transform.position.z);

        if (state == State.BURNING && fireLength == 0)
        {
            foreach (Animal a in animals)
            {
                a.health -= damage;
            }
            fireLength++;
        }

        if (state == State.BURNING)
        {
            fireLength++;
        }

        if (fireLength > explosionTime || this.transform.position.x < -5)
        {
            Destroy(currentExplosion);
            Destroy(this.gameObject);
        }
	}

    public override void CatchFire()
    {
        if(state != State.BURNING)
            currentExplosion = (GameObject) Instantiate(explosion, this.transform.position, new Quaternion());

        state = State.BURNING;
    }

    public override void Startle(float x, float z)
    {
        return;
    }
}
