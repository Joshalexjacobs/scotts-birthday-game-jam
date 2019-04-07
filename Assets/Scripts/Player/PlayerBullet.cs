using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed = 5.0f;
    public float damage = 1f;

    public GameObject bulletHit;

    private Rigidbody2D rigidbody2D;

    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();

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
        Destroy(gameObject);
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
