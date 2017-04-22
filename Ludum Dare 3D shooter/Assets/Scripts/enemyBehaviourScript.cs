using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviourScript : MonoBehaviour {

	public float enemySpeed;
    public float health;
    public float MaxHealth;
    public GameObject player;

        
        private void Update() {
        transform.LookAt(player.transform);

        transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);

        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void Damage(float damage) {
        health -= damage;
    }

}
