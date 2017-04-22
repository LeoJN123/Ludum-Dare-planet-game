using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehavior : MonoBehaviour {

    public float bulletSpeed;
    public float bulletDecay;
    public float bulletDamage;
    public GameObject explosion;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, bulletDecay);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
	}

    private void OnTriggerEnter(Collider collision) {

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        if (collision.GetComponent<enemyBehaviourScript>() != null) {
            collision.GetComponent<enemyBehaviourScript>().Damage(bulletDamage);
        }
    }
}
