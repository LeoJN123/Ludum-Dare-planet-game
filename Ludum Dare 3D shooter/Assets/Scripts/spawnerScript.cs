using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour {

    public enum WaveState { WaveInProgress, Intermission }
    public float enemySpawnTickTime = 0.1f;
    public float spawnTick = 0;
    public float spawnTickTimeDecreaser = 0.95f;
    public int waveNumber = 0;
    public float difficultyRaiser = 1.1f;
    public float enemiesInTotal = 5;
    public float enemiesToSpawn = 5;
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject enemy;
    public GameObject[] spawnPoints;
    public WaveState currentState = WaveState.Intermission;

    void Update() {

        if (currentState == WaveState.Intermission) {

            if (Input.GetButtonDown("Submit")) {
                //Starting a wave when you press Enter
                StartWave();

            }
        }

        if (currentState == WaveState.WaveInProgress) {
            //When wave is in progress, spawn enemies.
            if (enemiesToSpawn > 0) {
                SpawnEnemy();
            } else if (enemiesToSpawn == 0 && enemyList.Count == 0) {
                WaveEnd();
            }
            

        }
    }

    public void StartWave() {


        //Set current state to wave in progress
        currentState = WaveState.WaveInProgress;

        //Play animation of something here
        //Display stuff

        //Setting wave number up by 1
        waveNumber++;

        //Making monsters spawn quicker!

        //Multiply enemy number with difficulty
        enemiesInTotal = enemiesInTotal * difficultyRaiser;
        enemiesToSpawn = enemiesInTotal;
        enemySpawnTickTime = enemySpawnTickTime / spawnTickTimeDecreaser;

    }

    public void WaveEnd() {

        //Lets make sure our list of enemies is really empty
        enemyList.Clear();
        //set game to timeout
        currentState = WaveState.Intermission;

        //play animation?

    }

    public void SpawnEnemy() {

        spawnTick += Time.deltaTime;

        if (enemySpawnTickTime > 0) {
            while (spawnTick >= enemySpawnTickTime) {
                spawnTick = spawnTick - enemySpawnTickTime;
                
                    GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
                    enemiesToSpawn--;
                
            }
        }
    }


}
