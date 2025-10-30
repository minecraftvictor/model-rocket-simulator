using UnityEngine;


public class RocketScript : MonoBehaviour
{
   public Rigidbody rb;

    public Motor motor; 

    public float motorForce;

    public float distance;
    public Vector3 startPosition;

    void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
      distance = Vector3.Distance(startPosition, transform.position);
      Debug.Log(distance);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SelectMotor(Motor selectMotor)
    {
      motor = selectMotor;
      float force1 = motor.minumumForce;
      float force2 = motor.maxForce;
      motorForce = Random.Range(force1,force2);
    }

    public void LaunchRocket()
    {
      rb.AddForce(transform.up * motorForce, ForceMode.Impulse);
    }



}
