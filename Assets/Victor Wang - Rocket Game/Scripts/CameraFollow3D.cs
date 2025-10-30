// CameraFollow3D.cs
// Attach to Main Camera; simple vertical follow with offset.
using UnityEngine;

public class CameraFollow3D : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distance = 12f;
    [SerializeField] float minDistance = 6f;
    [SerializeField] float maxDistance = 30f;
    [SerializeField] float mouseSensitivity = 120f;
    [SerializeField] float scrollSensitivity = 4f;
    [SerializeField] float yMinLimit = -20f;
    [SerializeField] float yMaxLimit = 80f;
    [SerializeField] float smoothTime = 0.05f;

    float yaw;
    float pitch;
    Vector3 vel;

    void Start()
    {
        if (target)
        {
            Vector3 angles = transform.eulerAngles;
            yaw = angles.y;
            pitch = angles.x;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (!target) return;

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, yMinLimit, yMaxLimit);

        distance = Mathf.Clamp(distance - Input.mouseScrollDelta.y * scrollSensitivity, minDistance, maxDistance);

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPos = target.position - (rot * Vector3.forward * distance);

        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref vel, smoothTime);
        transform.rotation = rot;
    }
}
