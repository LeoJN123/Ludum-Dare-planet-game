using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyBehaviour : MonoBehaviour {

    public float myValue;
    public ForceMode moneyMovementForceMode;
    public float Attraction;
    GameObject player;
    Rigidbody rb;

    private void Awake() {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        Vector3 Target = player.transform.position - transform.position;
        rb.velocity = Vector3.zero;
        rb.AddForce(Target.normalized * Attraction, moneyMovementForceMode);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            other.GetComponent<playerBehaviourScript>().CollectMoney(myValue);
            Destroy(gameObject);
        }
    }
}
