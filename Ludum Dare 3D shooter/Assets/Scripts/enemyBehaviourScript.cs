using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviourScript : MonoBehaviour {

	public float enemySpeed;
    public GameObject player;

        
        private void Update() {
        transform.LookAt(player.transform);
    }

}
