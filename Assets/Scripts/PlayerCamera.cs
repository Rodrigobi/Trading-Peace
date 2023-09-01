using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target; // The player's transform
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Camera offset from the player
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement

    void LateUpdate()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;

        // Look at the player
        transform.LookAt(target);
    }
}
