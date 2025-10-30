// Controls: Space = Thrust, A/D = Yaw, W/S = Pitch, Q/E = Roll, R = Restart
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
     [Header("Config")]
    [SerializeField] RocketConfig config;
    [SerializeField] float gravity = -9.81f;

    [Header("FX (optional)")]
    [SerializeField] ParticleSystem engineVfx;
    [SerializeField] AudioSource engineAudio;

    [Header("Coasting")]
    [SerializeField] float coastingDragMultiplier = 3f; // extra drag when not thrusting

    public bool hasLaunched = false;

    Rigidbody body;
    float fuel;

    // cached inputs
    float yawIn, pitchIn, rollIn;
    bool thrustHeld;

    public float Fuel => fuel;
    public bool OutOfFuel => fuel <= 0f;
    public float Altitude => transform.position.y;
    public Vector3 Velocity => body ? body.linearVelocity : Vector3.zero;
    public float Speed => body ? body.linearVelocity.magnitude : 0f;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        fuel = config ? config.FuelCapacity : 10f;

        if (body)
        {
            body.useGravity = true;
            body.linearDamping = config ? config.Drag : 0.02f;
            body.angularDamping = config ? config.AngularDrag : 0.1f;
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }

    void Start()
    {
        Physics.gravity = new Vector3(0f, gravity, 0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        // WORLD-SPACE controls (no camera-relative behavior)
        rollIn =
            (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? -1f : 0f) +
            (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)  ?  1f : 0f);

        pitchIn =
            (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)    ?  1f : 0f) +
            (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)  ? -1f : 0f);

        yawIn =
            (Input.GetKey(KeyCode.E) ?  1f : 0f) +
            (Input.GetKey(KeyCode.Q) ? -1f : 0f);

        thrustHeld = Input.GetKey(KeyCode.Space);
        if (!hasLaunched && thrustHeld) hasLaunched = true;

        UpdateFx(thrustHeld && fuel > 0f);
    }

    void FixedUpdate()
    {
        ApplyRotation(yawIn, pitchIn, rollIn, Time.fixedDeltaTime);

        // Dynamic drag: higher when coasting to bleed velocity fast
        if (body)
        {
            float baseDrag = config ? config.Drag : 0.02f;
            body.linearDamping = (!thrustHeld || fuel <= 0f) ? baseDrag * coastingDragMultiplier : baseDrag;
        }

        if (thrustHeld && fuel > 0f)
            ApplyThrust(Time.fixedDeltaTime);
    }

    void ApplyRotation(float yaw, float pitch, float roll, float dt)
    {
        if (!body) return;

        float yawDelta   = (config ? config.YawDegreesPerSec   : 120f) * yaw   * dt;
        float pitchDelta = (config ? config.PitchDegreesPerSec :  90f) * pitch * dt;
        float rollDelta  = (config ? config.RollDegreesPerSec  :  60f) * roll  * dt;

        // Pure world axes (stable, non-camera-aware)
        Quaternion qYaw   = Quaternion.AngleAxis(yawDelta,   Vector3.up);
        Quaternion qPitch = Quaternion.AngleAxis(pitchDelta, Vector3.right);
        Quaternion qRoll  = Quaternion.AngleAxis(rollDelta,  Vector3.forward);

        Quaternion target = qYaw * body.rotation * qPitch * qRoll;
        body.MoveRotation(target);
    }

    // Real force (Newtons) so mass & gravity matter
    void ApplyThrust(float dt)
    {
        if (!body) return;

        float burn = Mathf.Min(fuel, (config ? config.BurnRatePerSec : 1.5f) * dt);
        fuel -= burn;

        float nominal = (config ? config.BurnRatePerSec : 1.5f) * dt;
        float throttle = nominal > 0f ? burn / nominal : 0f;

        float forceN = (config ? config.ThrustForce : 2200f) * throttle;

        Vector3 axis = transform.up;
        if (config && config.ThrustAxis == ThrustAxis3D.Forward)
            axis = transform.forward;

        body.AddForce(axis * forceN, ForceMode.Force);
    }

    void UpdateFx(bool on)
    {
        if (engineVfx)
        {
            if (on && OutOfFuel == false) engineVfx.Play();
        }
        if (engineAudio)
        {
            if (on && !engineAudio.isPlaying) engineAudio.Play();
            if (!on && engineAudio.isPlaying) engineAudio.Stop();
        }
    }

    public void RefillFuel()
    {
        fuel = config ? config.FuelCapacity : 10f;
    }
}
