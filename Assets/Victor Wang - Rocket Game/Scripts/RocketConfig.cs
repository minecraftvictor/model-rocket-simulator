using UnityEngine;


public enum ThrustAxis3D { Up, Forward }

[CreateAssetMenu(menuName = "Rocket/Rocket Config")]
public class RocketConfig : ScriptableObject
{
    [SerializeField] float thrustForce = 12000f;        // N (for mass=500, g=15 → TWR ~1.6)
    [SerializeField] float yawDegreesPerSec = 120f;
    [SerializeField] float pitchDegreesPerSec = 90f;
    [SerializeField] float rollDegreesPerSec = 60f;

    [SerializeField] float fuelCapacity = 20f;
    [SerializeField] float burnRatePerSec = 2.5f;

    [SerializeField] float drag = 0.06f;                // higher base drag
    [SerializeField] float angularDrag = 0.8f;
    [SerializeField] float coastingDragMultiplier = 4f; // applied when not thrusting
    [SerializeField] ThrustAxis3D thrustAxis = ThrustAxis3D.Up;

    public float ThrustForce => thrustForce;
    public float YawDegreesPerSec => yawDegreesPerSec;
    public float PitchDegreesPerSec => pitchDegreesPerSec;
    public float RollDegreesPerSec => rollDegreesPerSec;
    public float FuelCapacity => fuelCapacity;
    public float BurnRatePerSec => burnRatePerSec;
    public float Drag => drag;
    public float AngularDrag => angularDrag;
    public float CoastingDragMultiplier => coastingDragMultiplier;
    public ThrustAxis3D ThrustAxis => thrustAxis;
}
