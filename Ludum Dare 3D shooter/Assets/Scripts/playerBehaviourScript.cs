using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviourScript : MonoBehaviour {

    public float movePower;
    public float mouseSens;
    public float gravPower;
    public GameObject planet;
    private Rigidbody rb;
    Camera cam;
    

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    private void FixedUpdate() {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(movX, 0, movY);

        rb.AddForce(transform.forward * movY * movePower, ForceMode.Force);
        rb.AddForce(transform.right * movX * movePower, ForceMode.Force);

        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * -mouseSens;

        cam.transform.Rotate(new Vector3(mouseY * Time.deltaTime, 0, 0));
        //transform.Rotate(new Vector3(0, mouseX, 0));

        var q = Quaternion.AngleAxis(mouseX * Time.deltaTime, transform.up);

        Vector3 gravityDirecton = planet.transform.position - transform.position;
        rb.AddForce(gravityDirecton * gravPower);

        Vector3 Upward = transform.position - planet.transform.position;

        Vector3 newForward = Vector3.Cross(q * transform.right, Upward);

        rb.rotation = Quaternion.LookRotation(newForward, Upward);


    }
}

/*
    void Movement (Vector3 velocity) {
        rb.velocity += velocity;
    }

    Vector3 mDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));///new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    Vector3 mVelocity = mDirection.normalized * movePower;
    Movement(mVelocity);
    */
