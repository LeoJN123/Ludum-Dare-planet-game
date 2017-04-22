using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviourScript : MonoBehaviour {

    public float maxHealh;
    public float health;
    public float movePower;
    public float jumpPower;
    public float mouseSens;
    public float gravPower;
    public float gunRange;
    public float Spread;
    public float fireInterval;
    private float fireTimer;
    bool canFire;
    bool grounded;
    public GameObject planet;
    public GameObject bulletExit;
    public GameObject bullet;
    private Rigidbody rb;
    public LayerMask planetMask;
    public ParticleSystem muzzleEmit;
    Camera cam;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        muzzleEmit.Stop();
    }

    private void FixedUpdate() {

        //Movement
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(movX, 0, movY);
        Vector3 moveY = transform.forward * movY * movePower;
        Vector3 moveX = transform.right * movX * movePower;


        rb.AddForce(Vector3.Normalize(moveY + moveX) * Mathf.Max(moveY.magnitude, moveX.magnitude), ForceMode.Force);
        //rb.AddForce(transform.right * movX * movePower, ForceMode.Force);

        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * -mouseSens;

        cam.transform.Rotate(new Vector3(mouseY * Time.deltaTime, 0, 0));
        //transform.Rotate(new Vector3(0, mouseX, 0));

        var q = Quaternion.AngleAxis(mouseX * Time.deltaTime, transform.up);

        Vector3 gravityDirecton = planet.transform.position - transform.position;
        rb.AddForce(gravityDirecton * gravPower, ForceMode.Force);

        Vector3 Upward = transform.position - planet.transform.position;

        Vector3 newForward = Vector3.Cross(q * transform.right, Upward);

        rb.rotation = Quaternion.LookRotation(newForward, Upward);

        //Jumping

        var colliders = Physics.OverlapSphere(transform.position, 5f, planetMask);

        if (colliders.Length == 1) {
            grounded = true;
        }

        if (grounded && Input.GetButton("Jump")) {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            grounded = false;
        }

        

        //Shooting

        RaycastHit hit;

        //Ray ray = cam.ScreenPointToRay(new Vector3(.5f * cam.pixelWidth, .5f * cam.pixelHeight, 0));
        

        if (canFire && Input.GetButton("Fire1")) {

            Vector3 spread = new Vector3(Random.Range(-Spread, Spread),
                                         Random.Range(-Spread, Spread),
                                         Random.Range(-Spread, Spread));

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit)) {

                Vector3 rayHit = hit.point;
                GameObject newBullet = Instantiate(bullet, bulletExit.transform.position, Quaternion.LookRotation((hit.point - bulletExit.transform.position).normalized + spread));
                canFire = false;
            } else {
                GameObject newBullet = Instantiate(bullet, bulletExit.transform.position, Quaternion.LookRotation(cam.transform.forward + spread));
                canFire = false;
            }
        }
        

    }

    private void Update() {

        if (Input.GetButtonDown("Fire1")) {
            muzzleEmit.Play(true);
        }

        if (Input.GetButtonUp("Fire1")) {
            muzzleEmit.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        if (fireInterval > 0 && !canFire) {
            fireTimer += Time.deltaTime;

            while (fireTimer >= fireInterval) {
                canFire = true;

                fireTimer -= fireInterval;
            }
        }
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
