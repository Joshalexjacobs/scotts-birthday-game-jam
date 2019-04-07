using UnityEngine;

public class Shotgun : MonoBehaviour {

    private static float DISTANCE_BETWEEN_MOUSE = 5.23f;

    public Transform shootPoint;

    private SpriteRenderer spriteRenderer;
    private AudioSource[] audio;

    private Vector3 mousePosition;
    private Vector3 objectPosition;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audio = GetComponents<AudioSource>();
    }
	
	void Update () {
        LookAtMouse();
    }

    public void Shoot(GameObject bullet, float damage) {
        PlayerBullet playerBulletOne = Instantiate(bullet, shootPoint.position, transform.rotation).GetComponent<PlayerBullet>();
        PlayerBullet playerBulletTwo = Instantiate(bullet, shootPoint.position, transform.rotation).GetComponent<PlayerBullet>();
        PlayerBullet playerBulletThree = Instantiate(bullet, shootPoint.position, transform.rotation).GetComponent<PlayerBullet>();
        PlayerBullet playerBulletFour = Instantiate(bullet, shootPoint.position, transform.rotation).GetComponent<PlayerBullet>();
        PlayerBullet playerBulletFive = Instantiate(bullet, shootPoint.position, transform.rotation).GetComponent<PlayerBullet>();

        audio[0].Play();

        playerBulletOne.Init(damage);
        playerBulletTwo.Init(damage, new Vector3(0.1f, 0.1f, 0f));
        playerBulletThree.Init(damage, new Vector3(-0.1f, -0.1f, 0f));
        playerBulletFour.Init(damage, new Vector3(0.1f, 0.2f, 0f));
        playerBulletFive.Init(damage, new Vector3(-0.1f, -0.2f, 0f));
    }

    private void LookAtMouse() {
        mousePosition = Input.mousePosition;
        mousePosition.z = DISTANCE_BETWEEN_MOUSE;

        objectPosition = Camera.main.WorldToScreenPoint(transform.position);

        mousePosition.x = mousePosition.x - objectPosition.x;
        mousePosition.y = mousePosition.y - objectPosition.y;

        UpdatePlayerDirection(mousePosition.x);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg));
    }

    private void UpdatePlayerDirection(float mouseX) {
        Player player = FindObjectOfType<Player>();
        Vector3 playerPosition = player.transform.position;

        if (playerPosition.x > mouseX && !player.GetFlipped()) {
            spriteRenderer.flipY = !spriteRenderer.flipY;
            player.FlipPlayer();
        } else if (playerPosition.x < mouseX && player.GetFlipped()) {
            spriteRenderer.flipY = !spriteRenderer.flipY;
            player.FlipPlayer();
        }
    }
}
