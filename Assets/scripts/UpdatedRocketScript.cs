using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UpdatedRocketScript : MonoBehaviour
{
    public float thrustForce = 50f;      
    public float spinForce = 5f;         
    public float minUpSpeed = 1f;       
    public float flipTorque = 30f;      

    private Rigidbody rb;
    private bool burning = true;
    private bool flipped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass += new Vector3(0f, 0f, 0.1f);
        rb.AddTorque(Vector3.up * spinForce, ForceMode.Impulse);
    }
    void FixedUpdate()
    {
        if (burning)
        {

            rb.AddForce(transform.up * thrustForce, ForceMode.Force);

            if (rb.linearVelocity.y < minUpSpeed)
            {
                burning = false;
            }
        }
        else
        {

            if (!flipped)
            {
                rb.AddTorque(transform.right * flipTorque, ForceMode.Impulse);
                flipped = true;
            }

            rb.AddTorque(new Vector3(2f, 3f, 1f), ForceMode.Force);
        }
    }
}
