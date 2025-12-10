// SimpleHUD3D.cs
// Drop on a Canvas; wire TextMeshProUGUI fields (or leave null to skip).
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class RocketHUD : MonoBehaviour
{
    [SerializeField] Rocket rocket;
    [SerializeField] Image fuelBar;
    [SerializeField] TMP_Text fuelText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text altitudeText;

    [Space(10)]
    
    [SerializeField] GameObject keyboardControls;
    [SerializeField] GameObject touchControls;
    [SerializeField] Button quitGameButton;

    float maxFuel;

    void Start()
    {
        if (rocket != null)
        {
            maxFuel = rocket.Fuel;
        }

        // Show appropriate controls based on platform
        // #if UNITY_ANDROID || UNITY_IOS
        //     if (keyboardControls) keyboardControls.SetActive(false);
        //     if (touchControls) touchControls.SetActive(true);
        // #endif
        // #if UNITY_STANDALONE || UNITY_WEBGL
        //     if (keyboardControls) keyboardControls.SetActive(true);
        //     if (touchControls) touchControls.SetActive(false);
        // #endif

        keyboardControls.SetActive(rocket.useMobileControls == false);
        touchControls.SetActive(rocket.useMobileControls);
        quitGameButton.gameObject.SetActive(rocket.useMobileControls == false && Application.platform == RuntimePlatform.WebGLPlayer == false);

        if (quitGameButton)
        {
            quitGameButton.onClick.AddListener(() => Application.Quit());
        }
    }

    void Update()
    {
        if (!rocket) return;

        if (fuelText) fuelText.text = $"Fuel: {rocket.Fuel:0.0}";
        if (speedText) speedText.text = $"Speed: {rocket.Speed:0.0} m/s";
        if (altitudeText) altitudeText.text = $"Altitude: {rocket.Altitude:0.0} m";

        if (fuelBar) fuelBar.fillAmount = rocket.Fuel / maxFuel;
    }
}
