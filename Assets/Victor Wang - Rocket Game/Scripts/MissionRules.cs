// MissionRules3D.cs
// Attach once in the scene; set rocket reference and landing pad (Transform or tag).
using UnityEngine;
using UnityEngine.Events;

public class MissionRules : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Rocket rocket;
    [SerializeField] Transform[] landingPads;
    Transform targetLandingPad;

    [Header("Goals")]
    [SerializeField] float targetAltitude = 50f;
    [SerializeField] bool requireLanding = true;

    [Header("Landing Limits")]
    [SerializeField] float landingMaxSpeed = 4f;
    [SerializeField] float landingMaxTiltDegrees = 10f; // from straight-up

    [Header("Events")]
    [SerializeField] UnityEvent onReachAltitude;
    [SerializeField] UnityEvent onSafeLanding;
    [SerializeField] UnityEvent onCrash;

    public bool reachedAltitude;
    public bool missionOver;

    void Start()
    {
        if (landingPads != null && landingPads.Length > 0)
        {
            targetLandingPad = landingPads[Random.Range(0, landingPads.Length)];
            targetLandingPad.gameObject.SetActive(true);
            foreach (var pad in landingPads)
            {
                if (pad != targetLandingPad)
                {
                    pad.gameObject.SetActive(false);
                }
            }
        }
    }

    void Update()
    {
        if (!rocket || missionOver) return;

        if (!reachedAltitude && rocket.Altitude >= targetAltitude)
        {
            reachedAltitude = true;
            onReachAltitude?.Invoke();

            if (!requireLanding)
            {
                missionOver = true;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (!rocket || missionOver || !rocket.hasLaunched) return;

        bool isPad = targetLandingPad && col.collider.transform == targetLandingPad;
        if (!isPad)
        {
            onCrash?.Invoke();
            missionOver = true;
            return;
        }

        bool speedOk = rocket.Speed <= landingMaxSpeed;
        float tilt = Vector3.Angle(rocket.transform.up, Vector3.up);
        bool tiltOk = tilt <= landingMaxTiltDegrees;

        if (speedOk && tiltOk && (!requireLanding || reachedAltitude))
        {
            onSafeLanding?.Invoke();
            missionOver = true;
        }
        else
        {
            onCrash?.Invoke();
            missionOver = true;
        }
    }
}
