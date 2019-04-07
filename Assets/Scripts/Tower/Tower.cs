using UnityEngine;

public class Tower : MonoBehaviour {

    public float health = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
    public void Damage(float damage) {
        health -= damage;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
