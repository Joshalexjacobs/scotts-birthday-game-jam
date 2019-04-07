using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public float health = 100f;
    public float shotTimerMax = 1f;

    public GameObject laser;
    public GameObject towerShotgun;

    private float shotTimer = 0f;

    // Use this for initialization
    void Start () {
        shotTimer = shotTimerMax;
	}
	
    public void Damage(float damage) {
        health -= damage;
    }

	// Update is called once per frame
	void Update () {
        CheckForEnemies();
    }

    private void CheckForEnemies() {
        if (shotTimer >= shotTimerMax) {
            bool hasShot = false;

            List<Collider2D> colliders = new List<Collider2D>(Physics2D.OverlapCircleAll(transform.position, 1f));

            colliders.ForEach(collider => {
                Enemy enemy = collider.GetComponent<Enemy>();

            });

            colliders.ForEach(collider => {
                if (collider.tag == "Enemy" && !hasShot)
                {
                    Enemy enemy = collider.GetComponent<Enemy>();

                    if (!enemy.isDead)
                    {
                        Vector3 objectPosition = enemy.transform.position;
                        Vector3 towerPosition = towerShotgun.transform.position;

                        objectPosition.x = objectPosition.x - towerPosition.x;
                        objectPosition.y = objectPosition.y - towerPosition.y;

                        towerShotgun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(objectPosition.y, objectPosition.x) * Mathf.Rad2Deg));

                        PlayerBullet playerBullet = Instantiate(laser, towerShotgun.transform.position, towerShotgun.transform.rotation).GetComponent<PlayerBullet>();
                        playerBullet.Init(1f);

                        hasShot = true;
                        shotTimer = 0f;
                    }
                }
            });
        } else {
            shotTimer += Time.deltaTime;
        }
    }
}
