using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviourScript : MonoBehaviour {

    public float movePower;
    public float mouseSens;
    private Rigidbody rb;
    Camera cam;
    

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    private void FixedUpdate() {

        Vector3 mDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 mVelocity = mDirection.normalized * movePower;
        Movement(mVelocity);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Look(mouseX, mouseY);
    }

    void Movement (Vector3 velocity) {
        rb.velocity += velocity;
    }

    void Look (float mX, float mY) {
        Quaternion camRot = Quaternion.Euler(mY, 0, 0);
        Quaternion playerRot = Quaternion.Euler(0, mX, 0);

        print(mX + " " + mY);
    }
}
