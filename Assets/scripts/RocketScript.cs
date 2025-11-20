using UnityEngine;


public class RocketScript : MonoBehaviour
{
    public Rigidbody rb;
    public Motor motor; 

    public float motorForce;

    public float distance;
    public Vector3 startPosition;
    public GameObject startObject;

    public float initialDistance;
    
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
      if(launched){
        distance = Vector3.Distance(startPosition, transform.position) - initialDistance;
      }
      
    }

    void FixedUpdate()
    {
        if (launched == true)
        {
          if(flipped == false && engineVfx != null)
            {
              engineVfx.Play();
            }
         
          if (rb.linearVelocity.y < 0 && flipped == false)
          {
            Debug.Log("Falling");
            if(engineVfx != null){
              engineVfx.Stop();
            }
            rb.AddTorque(transform.right * 65f, ForceMode.Force);
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
      startPosition = startObject.transform.position;
      initialDistance = Vector3.Distance(startPosition, transform.position);
      Debug.Log("Launched");
      rb.AddForce(transform.up * motorForce, ForceMode.Impulse);
      launched = true;
    }

}
