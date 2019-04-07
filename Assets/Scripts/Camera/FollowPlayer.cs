using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
    //public Vector3 offset;
    public float followSpeed = 1f;

	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = player.position;
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }
}
