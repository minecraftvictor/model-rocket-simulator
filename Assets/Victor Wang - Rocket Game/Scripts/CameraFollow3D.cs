// CameraFollow3D.cs
// Attach to Main Camera; simple vertical follow with offset.
using UnityEngine;

public class CameraFollow3D : MonoBehaviour
{
    [SerializeField] Rocket target;
    [SerializeField] float distance = 12f;
    [SerializeField] float minDistance = 6f;
    [SerializeField] float maxDistance = 30f;

    [Header("Mouse")]
    [SerializeField] float mouseSensitivity = 120f;
    [SerializeField] bool holdRightMouseToLook = false;

    [Header("Touch")]
    [SerializeField] float touchLookSensitivity = 0.2f;      // degrees per pixel
    [SerializeField] float pinchZoomSensitivity = 0.02f;     // units per pixel delta

    [Header("Common")]
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
        // No cursor lock per request
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (!target) return;

        HandleRotation();
        HandleZoom();

        pitch = Mathf.Clamp(pitch, yMinLimit, yMaxLimit);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPos = target.transform.position - (rot * Vector3.forward * distance);

        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref vel, smoothTime);
        transform.rotation = rot;
    }

    void HandleRotation()
    {
        // Touch drag look
        if (target.useMobileControls && Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                var t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Moved)
                {
                    yaw   += t.deltaPosition.x * touchLookSensitivity;
                    pitch -= t.deltaPosition.y * touchLookSensitivity;
                }
            }
            else
            {
                // If more than one finger is down, ignore rotation here (pinch will handle zoom)
            }
            return; // prefer touch when present
        }

        // Mouse look (no lock). Optionally require RMB.
        bool allowMouseLook = !holdRightMouseToLook || Input.GetMouseButton(1);
        if (allowMouseLook)
        {
            yaw   += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        }
    }

    void HandleZoom()
    {
        // Pinch to zoom
        if (target.useMobileControls && Input.touchCount >= 2)
        {
            var t0 = Input.GetTouch(0);
            var t1 = Input.GetTouch(1);

            Vector2 t0Prev = t0.position - t0.deltaPosition;
            Vector2 t1Prev = t1.position - t1.deltaPosition;

            float prevMag = (t0Prev - t1Prev).magnitude;
            float currMag = (t0.position - t1.position).magnitude;
            float delta = currMag - prevMag; // >0 fingers apart, <0 fingers together

            distance -= delta * pinchZoomSensitivity;
            return; // prefer touch when present
        }

        // Mouse wheel zoom
        distance -= Input.mouseScrollDelta.y * scrollSensitivity;
    }
}
