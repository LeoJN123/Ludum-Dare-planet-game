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

        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        cam.transform.Rotate(new Vector3(mouseY, 0, 0));
        transform.Rotate(new Vector3(0, mouseX, 0));
    }

    void Movement (Vector3 velocity) {
        rb.velocity += velocity;
    }
}
