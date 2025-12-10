using TMPro;
using UnityEngine;


public class RocketScript : MonoBehaviour
{
  public Rigidbody rb;
  public Motor motor;

  public float motorForce;

  public float distance;
  public float startPosition;
  public GameObject startObject;

  public TMP_Text launchInfoText;

  [SerializeField] ParticleSystem engineVfx;


  public bool launched = false;
  public bool flipped = false;
  public float minUpSpeed = 1f;

  float timer;
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    launched = false;

    UpdateInformation(0f, 0f);
  }

  void Update()
  {
    if (launched)
    {
      distance = transform.position.y - startPosition;

      // Time since launch
      timer += Time.deltaTime;
      UpdateInformation(distance, timer);
    }
  }

  void FixedUpdate()
  {
    if (launched == true)
    {
      if (flipped == false && engineVfx != null)
      {
        engineVfx.Play();
      }

      if (rb.linearVelocity.y < 0 && flipped == false)
      {
        Debug.Log("Falling");
        if (engineVfx != null)
        {
          engineVfx.Stop();
        }
        rb.AddTorque(transform.right * 65f, ForceMode.Force);
        flipped = true;
      }
    }
  }

  void UpdateInformation(float distance, float time)
    {
      // distance format as "Altitude: XXXX m"
      // time format as "Time: XX.XX s"
      launchInfoText.text = "Altitude: " + distance.ToString("F2") + " m\n" + "Time: " + time.ToString("F2") + " s";
    }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  public void SelectMotor(Motor selectMotor)
  {
    motor = selectMotor;
    float force1 = motor.minumumForce;
    float force2 = motor.maxForce;
    motorForce = Random.Range(force1, force2);
  }

  public void LaunchRocket()
  {
    startPosition = startObject.transform.position.y;
    Debug.Log("Launched");
    rb.AddForce(transform.up * motorForce, ForceMode.Impulse);
    launched = true;
    timer = 0f;
  }

}
