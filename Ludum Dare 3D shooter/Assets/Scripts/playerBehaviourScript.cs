using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviourScript : MonoBehaviour {

    public enum GameState { Paused, Playing };

    public float maxHealh;
    public float health;
    public float movePower;
    public float maxJumpPower;
    public float jumpPower;
    public float mouseSens;
    public float gravPower;
    public float gunRange;
    public float Spread;
    public float fireInterval;
    private float fireTimer;
    public GameState state;
    bool canFire;
    bool grounded;
    public GameObject planet;
    public GameObject bulletExit;
    public GameObject bullet;
    private Rigidbody rb;
    public LayerMask planetMask;
    public ParticleSystem muzzleEmit;
    public Animation gunRecoil;
    Camera cam;
    private float cameraX = 0;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        muzzleEmit.Stop();
        state = GameState.Playing;
    }

    private void FixedUpdate() {

        if (state == GameState.Playing) {

            Movement();
            Jumping();
            Shooting();
        }
    }

    private void Update() {

        ShootInterval();

        //Shooting particle effects

        if (Input.GetButtonDown("Fire1") && canFire) {
            muzzleEmit.Play(true);
        } else { muzzleEmit.Stop(true, ParticleSystemStopBehavior.StopEmitting); }

    }

    public void Damaged(float damage) {
        health -= damage;
        print("Our health: " + health);
        if (health <= 0) {
            print("You're supposed to be dead by now");
        }
    }

    public void Movement() {

        //Getting all our inputs
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
        Vector3 moveY = transform.forward * movY * movePower;
        Vector3 moveX = transform.right * movX * movePower;

        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * -mouseSens;


        //Moving our mouse
        cameraX = Mathf.Clamp(cameraX + mouseY * Time.deltaTime, -85, 85);
        cam.transform.localRotation = Quaternion.Euler(cameraX,
                                                       cam.transform.localRotation.y , 
                                                       cam.transform.localRotation.z);

        //Movement and planetary gravity stuff

        rb.AddForce(Vector3.Normalize(moveY + moveX) * Mathf.Max(moveY.magnitude, moveX.magnitude), ForceMode.Force);

        var q = Quaternion.AngleAxis(mouseX * Time.deltaTime, transform.up);

        Vector3 gravityDirecton = planet.transform.position - transform.position;
        rb.AddForce(gravityDirecton * gravPower, ForceMode.Force);

        Vector3 Upward = transform.position - planet.transform.position;

        Vector3 newForward = Vector3.Cross(q * transform.right, Upward);

        rb.rotation = Quaternion.LookRotation(newForward, Upward);
    }

    public void Shooting() {

        RaycastHit hit;

        //Shootan
        if (canFire && Input.GetButton("Fire1")) {

            Vector3 spread = new Vector3(Random.Range(-Spread, Spread),
                                         Random.Range(-Spread, Spread),
                                         Random.Range(-Spread, Spread));

            //Playing our animation
            gunRecoil.Play();
            muzzleEmit.Play(true);

            //Making sure our bullets are directed at whatever is on the crosshair
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit)) {

                Vector3 rayHit = hit.point;
                GameObject newBullet = Instantiate(bullet, 
                                                   bulletExit.transform.position, 
                                                   Quaternion.LookRotation((hit.point - bulletExit.transform.position).normalized + spread));
                canFire = false;

            } else {
                GameObject newBullet = Instantiate(bullet, 
                                                   bulletExit.transform.position, 
                                                   Quaternion.LookRotation(cam.transform.forward + spread));
                canFire = false;
            }
        }
    }

    public void ShootInterval() {

        //Can't shoot too fast
        if (fireInterval > 0 && !canFire) {
            fireTimer += Time.deltaTime;

            while (fireTimer >= fireInterval) {
                canFire = true;

                fireTimer -= fireInterval;
            }
        }
    }

    public void Jumping() {

        //Jumping
        //Checking our boots are on the ground
        var colliders = Physics.OverlapSphere(transform.position, 5f, planetMask);

        if (colliders.Length == 1) {
            grounded = true;
        }
        if (Input.GetButton("Jump")) {
            jumpPower -= Time.deltaTime;
            rb.AddForce(transform.up * jumpPower, ForceMode.VelocityChange);

            if (Input.GetButtonDown("Jump") && grounded) {
                grounded = false;
                jumpPower = maxJumpPower;
            }
        }
    }
}
