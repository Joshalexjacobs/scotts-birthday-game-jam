using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed = 5.0f;
    public float damage = 1f;

    public GameObject bulletHit;

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circle;

    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();

        StartCoroutine("Die");
    }
	
    public void Init(float damage) {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(transform.right * speed);
        this.damage = damage;
    }

    public void Init(float damage, Vector3 mod) {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce((mod + transform.right) * speed);
        this.damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Enemy") {
            collision.gameObject.GetComponent<Enemy>().Damage(damage);
        }

        Instantiate(bulletHit, transform.position, transform.rotation);

        GetComponent<ParticleSystem>().Stop();

        circle.enabled = false;
        spriteRenderer.enabled = false;
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
