using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool isDead = false;
    public float damage = 1f;
    public float speed = 5f;
    public float shootTimerMax = 1f;

    public GameObject bullet;
    public Shotgun shotgun;

    private float shootTimer = 0f;

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private GameObject camera;

    private ScreenShake screenShake;

    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        camera = GameObject.FindGameObjectWithTag("MainCamera");
        screenShake = camera.GetComponent<ScreenShake>();

        shootTimer = shootTimerMax;
    }

    private void Update() {
        HandleShooting(Input.GetAxis("Fire1") > 0 && shootTimer >= shootTimerMax);
    }

    void FixedUpdate () {
        float dx = HandleMovement(Input.GetAxisRaw("Horizontal") != 0, rigidbody2D.velocity.x, "Horizontal");
        float dy = HandleMovement(Input.GetAxisRaw("Vertical") != 0, rigidbody2D.velocity.y, "Vertical");

        rigidbody2D.velocity = new Vector2(dx, dy);
    }

    /* SHOOTING */

    private void HandleShooting(bool isShooting) {
        if(isShooting) {
            shotgun.Shoot(bullet, damage);
            screenShake.TriggerShake(0.2f);
            shootTimer = 0f;
        }

        IncrementShootTimer();
    }

    private void IncrementShootTimer() {
        if (shootTimer < shootTimerMax) {
            shootTimer += Time.deltaTime;
        }
    }

    /* MOVEMENT */

    private float HandleMovement(bool isMoving, float movement, string axis) {
        if(isMoving) {
            movement = speed * Input.GetAxis(axis);
        }

        return movement;
    }

    /* OTHER */

    public bool GetFlipped() {
        return spriteRenderer.flipX;
    }

    public void FlipPlayer() {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
