using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour {

    public float enemySpawnTime;
    public float spawnTimer;
    public GameObject enemy;

    void Update () {
        spawnTimer += Time.deltaTime;

        if (enemySpawnTime != 0) {
            while (spawnTimer >= enemySpawnTime) {
                Instantiate(enemy, transform.position, Quaternion.identity);
                spawnTimer -= enemySpawnTime;
            }
        }
	}
}
