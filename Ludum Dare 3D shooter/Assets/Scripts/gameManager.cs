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

    private void Awake() {

        Cursor.visible = false;
        player = GameObject.Find("Player");
        pBS = player.GetComponent<playerBehaviourScript>();
        sC = GetComponent<spawnerScript>();
    }

    private void Update() {

        health.text = "" + pBS.health;
        wave.text = "" + sC.waveNumber;
  
        /*if (sC.currentState == spawnerScript.WaveState.Intermission) {
            if (Input.GetButtonDown("Shop")) {

            }
        }*/
    }

}
