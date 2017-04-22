using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletExplosionScript : MonoBehaviour {

    public float emitterTime;

    private void Awake() {
        Destroy(gameObject, emitterTime);
    }
}
