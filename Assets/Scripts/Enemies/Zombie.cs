using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy {

    public Zombie() {
        health = 1f;
        isDead = false;
    }

    public float speed = 1.0f;
    public bool chasingTower = true;
    public float attackTimerMax = 5f;

    public GameObject bite;

    private float attackTimer = 0f;
    private Tower tower;
    private Player player;

    private GameObject camera;
    private ScreenShake screenShake;

    private SpriteRenderer spriteRenderer;

    void Start () {
        tower = FindObjectOfType<Tower>();
        player = FindObjectOfType<Player>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        camera = GameObject.FindGameObjectWithTag("MainCamera");
        screenShake = camera.GetComponent<ScreenShake>();

        attackTimer = attackTimerMax;
    }

    void Update() {
        if (!isDead) {
            if (chasingTower) {
                MoveTowardsObject(tower.transform, true);
                LookForPlayer();
            } else {
                MoveTowardsObject(player.transform, false);
            }
        }
    }

    private void MoveTowardsObject(Transform target, bool movingTowardsTower) {
        float step = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) > 0.25f) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        } else if (attackTimer >= attackTimerMax) {
            AttackTower();
        }

        if (movingTowardsTower && attackTimer < attackTimerMax) {
            attackTimer += Time.deltaTime;
        }

        HandleZombieFlip(target);
    }

    private void AttackTower() {
        Instantiate(bite, tower.transform.position, Quaternion.identity);
        tower.Damage(1f);
        attackTimer = 0f;
        screenShake.TriggerShake(0.05f);
    }

    private void LookForPlayer() {
        bool towerIsClose = false;
        bool playerIsClose = false;

        List<Collider2D> colliders = new List<Collider2D> (Physics2D.OverlapCircleAll(transform.position, 1f));

        colliders.ForEach(collider => {
            if (collider.transform.tag == "Player") {
                playerIsClose = true;
            }

            if (collider.tag == "Tower") {
                towerIsClose = true;
            }
        });

        if(!towerIsClose && playerIsClose) {
            chasingTower = false;
        }
    }

    private void HandleZombieFlip(Transform target) {
        if (transform.position.x > target.position.x && !spriteRenderer.flipX) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        } else if (transform.position.x < target.position.x && spriteRenderer.flipX) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
