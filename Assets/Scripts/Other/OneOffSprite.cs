using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOffSprite : MonoBehaviour {

    public float deathTime;

	// Use this for initialization
	void Start () {
        StartCoroutine("Die");
	}
	
    IEnumerator Die() {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
