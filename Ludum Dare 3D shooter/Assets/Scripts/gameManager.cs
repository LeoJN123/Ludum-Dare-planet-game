using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

    GameObject player;
    playerBehaviourScript pBS;
    spawnerScript sC;
    public Text health;
    public Text wave;
    public Text enemiesRemaining;
    public Text upgradePoints;
    public GameObject Shop;
    public Text StoreSign;
    public Image startScreen;
    public Image blobUI;

    private void Awake() {


        Cursor.visible = false;
        player = GameObject.Find("Player");
        pBS = player.GetComponent<playerBehaviourScript>();
        sC = GetComponent<spawnerScript>();
        pBS.state = playerBehaviourScript.GameState.Paused;
        blobUI.enabled = false;
        health.enabled = false;
        wave.enabled = false;
        enemiesRemaining.enabled = false;
        upgradePoints.enabled = false;
        StoreSign.enabled = false;

    }

    private void Update() {
        
            if (Input.GetKey(KeyCode.Return)) {
                startScreen.enabled = false;
                pBS.state = playerBehaviourScript.GameState.Playing;
            blobUI.enabled = true;
            health.enabled = true;
            wave.enabled = true;
            enemiesRemaining.enabled = true;
            upgradePoints.enabled = true;

            }
        /*
        if (Shop.active = true && StoreSign.enabled == true) {
            pBS.state = playerBehaviourScript.GameState.Paused;
        }
        
        if (Shop.active && pBS.state == playerBehaviourScript.GameState.Paused) {
            if (Input.GetButtonDown("Shop")) {
                Shop.active = false;
            }
        }*/

        if (sC.currentState == spawnerScript.WaveState.WaveInProgress) {
            Shop.active = false;
            Cursor.visible = false;
        }

        if (pBS.state == playerBehaviourScript.GameState.Playing) {

            health.text = "" + (int)pBS.health;
            wave.text = "" + (int)sC.waveNumber;
            enemiesRemaining.text = sC.enemyList.Count + " Enemies left";
            upgradePoints.text = "" + (int)pBS.Money;
            if (sC.currentState == spawnerScript.WaveState.Intermission) {
                StoreSign.enabled = true;
                if (Input.GetButtonDown("Shop")) {
                    Shop.active = !Shop.active;
                    Cursor.visible = !Cursor.visible;
                }
            } else { StoreSign.enabled = false; }
        }
    }

}
