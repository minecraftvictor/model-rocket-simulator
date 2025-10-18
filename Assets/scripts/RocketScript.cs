using UnityEngine;

public class RocketScript : MonoBehaviour
{
   public Rigidbody rb;

    public Motor motor; 

    void Start()
    {
      rb = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SelectMotor(Motor selectMotor)
    {
      motor = selectMotor;
    }

    public void LaunchRocket(float force)
    {
    rb.AddForce(transform.up * force, ForceMode.Impulse);
    }



}
