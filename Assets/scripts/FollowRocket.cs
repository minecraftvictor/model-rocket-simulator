using UnityEngine;

public class CameraFollowRocket : MonoBehaviour
{
    public Transform rocket;        
    public Vector3 offset = new Vector3(0, 5, -10); 
    public float followSpeed = 5f;  

    void LateUpdate()
    {
        if (rocket == null) return;

        Vector3 targetPos = rocket.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

        transform.LookAt(rocket);
    }
}
