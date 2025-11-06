using UnityEngine;


public class RocketScript : MonoBehaviour
{
    public Rigidbody rb;
    public Motor motor; 

    public float motorForce;

    public float distance;
    public Vector3 startPosition;
    
    [SerializeField] ParticleSystem engineVfx;


    public bool launched = false;
    public bool flipped = false;
    public float minUpSpeed = 1f;
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      launched = false;
    }

    void Update()
    {
      distance = Vector3.Distance(startPosition, transform.position);
    }

    void FixedUpdate()
    {
        if (launched == true)
        {
          if(flipped == false)
            {
              engineVfx.Play();
            }
         
          if (rb.linearVelocity.y < 0 && flipped == false)
          {
            Debug.Log("Falling");
            engineVfx.Stop();
            rb.AddTorque(transform.right * 55f, ForceMode.Force);
            flipped = true;
          }
        }
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
      launched = true;
    }

}
