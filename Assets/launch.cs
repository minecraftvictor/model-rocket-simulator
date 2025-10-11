using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launch : MonoBehaviour
{
    public float thrust;
    public Rigidbody rb;

    public bool accceleration;
    public bool impulse;
    public bool force;
    public bool velocityChange;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(accceleration){
            rb.AddForce(Vector3.up * thrust, ForceMode.Acceleration);
        }
        if(impulse)
        {
            rb.AddForce(Vector3.up * thrust, ForceMode.Impulse);
        }
        if(force)
        {
            rb.AddForce(Vector3.up * thrust, ForceMode.Force);
        }
        if(velocityChange){
            rb.AddForce(Vector3.up * thrust, ForceMode.VelocityChange);
        }
    }

}