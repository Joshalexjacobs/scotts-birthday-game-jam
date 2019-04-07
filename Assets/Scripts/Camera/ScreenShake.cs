using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    public bool isShaking = false;
    public float shakeMagnitude = 0.7f;
    public float dampingSpeed = 1.0f;

    private float shakeDuration = 0f;
    private Vector3 initialPosition;

    void OnEnable() {
        initialPosition = transform.localPosition;
    }

    void Update() {
        if(isShaking) {
            if (shakeDuration > 0) {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

                shakeDuration -= Time.deltaTime * dampingSpeed;
            } else {
                shakeDuration = 0f;
                transform.localPosition = initialPosition;
                isShaking = false;
            }
        }
    }

    public void TriggerShake(float duration) {
        initialPosition = transform.localPosition;
        shakeDuration = duration;
        isShaking = true;
    }
}
