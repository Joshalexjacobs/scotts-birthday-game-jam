using UnityEngine;

public class Zombie : Enemy {

    public Zombie() {
        health = 1f;
        isDead = false;
    }

    public float speed = 1.0f;
    public bool chasingTower = true;
    public float attackTimerMax = 5f;

    private float attackTimer = 0f;
    private Tower tower;

    private SpriteRenderer spriteRenderer;

    void Start () {
        tower = FindObjectOfType<Tower>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        attackTimer = attackTimerMax;
    }

    void Update() {
        if (!isDead) {
            if (chasingTower) {
                MoveTowardsTower();
                // LookForPlayer();
            }

        }
    }

    private void MoveTowardsTower() {
        float step = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, tower.transform.position) > 0.25f) {
            transform.position = Vector3.MoveTowards(transform.position, tower.transform.position, step);
        } else if (attackTimer >= attackTimerMax) {
            tower.Damage(1f);
            attackTimer = 0f;
        }

        if (attackTimer < attackTimerMax) {
            attackTimer += Time.deltaTime;
        }

        HandleZombieFlip(tower.transform);
    }

    private void LookForPlayer() {
        // if player is within radius and the tower isnt,
        // move towards the player
    }

    private void HandleZombieFlip(Transform target) {
        if (transform.position.x > target.position.x && !spriteRenderer.flipX) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        } else if (transform.position.x < target.position.x && spriteRenderer.flipX) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
