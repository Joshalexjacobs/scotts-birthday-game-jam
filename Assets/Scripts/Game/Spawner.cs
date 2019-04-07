using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public List<Transform> spawnPoints;
    public GameObject zombie;

	// Use this for initialization
	void Start () {
        StartCoroutine("SpawnZombies");
	}
	
    IEnumerator SpawnZombies() {
        while(true) {
            int numberOfZombies = Random.Range(4, 10);

            for (int i = 0; i < numberOfZombies; i++) {
                Instantiate(zombie, spawnPoints[Random.Range(0, spawnPoints.Count)].position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            }

            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }
}
