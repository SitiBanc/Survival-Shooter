using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    // Our following target's transform
    public float smoothing;     // Smooth the camera following speed

    private Vector3 offset;     // The initial distance between camera and target

    // Use this for initialization
    void Start() {
        // Get initial offset value
        offset = transform.position - target.position;
    }

    void FixedUpdate() {
        // The target position our camera is going to
        Vector3 targetCamPos = offset + target.position;
        // Smoothly move our camera to its target position using Vector3.Lerp()（線性插值）
        // Smoothing * Time.deltaTime cause we want to move our camera smooth and slow (per second speed)
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
