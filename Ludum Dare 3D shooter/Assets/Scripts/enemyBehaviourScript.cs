using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviourScript : MonoBehaviour {

	public float enemySpeed;
    public float health;
    public float enemyDamage;
    public float EnemyRiches;
    public float enemyPushForce;
    public float impulseTickTime;
    private float impulseTick = 5;
    public ForceMode movementForceMode;
    Rigidbody rb;
    GameObject player;
    public GameObject money;
    spawnerScript spawnScript;
    Vector3 target;

    private void Awake() {
        //Finding Target
        player = GameObject.Find("Player");

        //Making reference to spawner Script
        spawnScript = GameObject.Find("gameManager").GetComponent<spawnerScript>();

        //Adding ourself to the list
        spawnScript.enemyList.Add(gameObject);

        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate() {

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

        //Drop money
        for (int i = 0; i < EnemyRiches; i++) {
            Instantiate(money, transform.position, Quaternion.identity);
        }

        //Death
        Destroy(gameObject);

        
    }

    public void Movement() {

        //Looking at the player and moving forward

        transform.LookAt(player.transform);

        target = player.transform.position - transform.position;

        impulseTick += Time.deltaTime;

        while (impulseTick >= impulseTickTime) {
            rb.velocity = Vector3.zero;
            rb.AddForce(target.normalized * enemySpeed, movementForceMode);
            impulseTick -= impulseTickTime;
        }

        
    }
    
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Player") {
            collision.gameObject.GetComponent<playerBehaviourScript>().Damaged(enemyDamage);
            print("We're hitting the player!!");
            collision.gameObject.GetComponent<Rigidbody>().AddForce(target.normalized * enemyPushForce, ForceMode.Impulse);
        }
    }


}
