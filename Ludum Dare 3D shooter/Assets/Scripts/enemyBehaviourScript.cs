using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviourScript : MonoBehaviour {

	public float enemySpeed;
    public float health;
    public float MaxHealth;
    public float enemyDamage;
    GameObject player;
    spawnerScript spawnScript;

    private void Awake() {
        //Finding Target
        player = GameObject.Find("Player");

        //Making reference to spawner Script
        spawnScript = GameObject.Find("gameManager").GetComponent<spawnerScript>();

        //Adding ourself to the list
        spawnScript.enemyList.Add(gameObject);
    }


    private void Update() {

        //Moving ourself
        Movement();

        
    }

    public void Damage(float damage) {

        //Taking damage out of our health
        health -= damage;

        //When we're at zero health, we die
        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        //Removing ourselves from the list of living enemies
        spawnScript.enemyList.Remove(gameObject);

        //Death
        Destroy(gameObject);
    }

    public void Movement() {

        //Looking at the player and moving forward

        transform.LookAt(player.transform);

        transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
    }
    
    private void OnTiggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<playerBehaviourScript>().Damaged(enemyDamage);
            print("We're hitting the player!!");
        }
    }


}
