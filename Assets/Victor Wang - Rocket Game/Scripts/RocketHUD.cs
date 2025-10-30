// SimpleHUD3D.cs
// Drop on a Canvas; wire TextMeshProUGUI fields (or leave null to skip).
using UnityEngine;
using TMPro;

public class RocketHUD : MonoBehaviour
{
    [SerializeField] Rocket rocket;
    [SerializeField] TMP_Text fuelText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text altitudeText;
    [SerializeField] TMP_Text statusText;

    void Update()
    {
        if (!rocket) return;

        if (fuelText) fuelText.text = $"Fuel: {rocket.Fuel:0.0}";
        if (speedText) speedText.text = $"Speed: {rocket.Speed:0.0} m/s";
        if (altitudeText) altitudeText.text = $"Alt: {rocket.Altitude:0.0} m";
    }

    public void SetStatus(string msg)
    {
        if (statusText) statusText.text = msg;
    }

    public void MsgReachedAltitude()
    {
        SetStatus("Reached target altitude!");
    }

    public void MsgLanded()
    {
        SetStatus("Safe landing — Mission Complete!");
    }

    public void MsgCrashed()
    {
        SetStatus("Crashed — Press R to restart");
    }
}
