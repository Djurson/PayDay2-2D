using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum playerAimState
{
    Aiming,
    NotAiming
};

public enum playerGunsState
{
    Primary,
    Secondary,
    Melee
};

public class PlayerGunHandler : MonoBehaviour
{
    [Header("Enums")]
    public playerAimState aimState;
    public playerGunsState gunState;

    private PlayerControls playerInput;

    [Header("Gun Refrences")]
    [SerializeField] private GameObject PrimaryWeapon;
    [SerializeField] private GameObject SecondaryWeapon;
    
    [Header("Aim Fx")]
    [SerializeField] private Camera cam;
    [SerializeField] private float ZoomIn;
    [SerializeField] private float ZoomOut;
    [SerializeField] private VolumeProfile fxProfile;
    [SerializeField] private Vignette vignetteFx;
    [SerializeField] private ChromaticAberration chromaticAberrationFx;
    [SerializeField] private LensDistortion lensDistortionFx;
    [SerializeField] private Bloom bloomFx;

    private void Awake()
    {
        playerInput = new PlayerControls();
        playerInput.Enable();
    }

    private void Start()
    {
        cam = Camera.main;
        fxProfile = GameObject.Find("PostProcessing").GetComponent<Volume>().profile;
        fxProfile.TryGet<Vignette>(out vignetteFx);
        fxProfile.TryGet<ChromaticAberration>(out chromaticAberrationFx);
        fxProfile.TryGet<LensDistortion>(out lensDistortionFx);
        fxProfile.TryGet<Bloom>(out bloomFx);
    }

    private void Update()
    {
        playerInput.KeyboardInputs.Aim.performed += Aim;
        playerInput.KeyboardInputs.Aim.canceled += StopAiming;
        playerInput.KeyboardInputs.SwitchPrimaryWeapon.performed += SwitchToPrimary;
        playerInput.KeyboardInputs.SwitchSecondary.performed += SwitchToSecondary;
        playerInput.KeyboardInputs.MouseSwitchWeapon.performed += SwitchWeapon;

        if(aimState == playerAimState.Aiming)
        {
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, ZoomIn, 5 * Time.deltaTime);
            vignetteFx.intensity.value = Mathf.MoveTowards(vignetteFx.intensity.value, 0.4f, Time.deltaTime);
            chromaticAberrationFx.intensity.value = Mathf.MoveTowards(chromaticAberrationFx.intensity.value, 0.35f, Time.deltaTime);
            lensDistortionFx.intensity.value = Mathf.MoveTowards(lensDistortionFx.intensity.value, -0.25f, Time.deltaTime);
            bloomFx.intensity.value = Mathf.MoveTowards(bloomFx.intensity.value, 1.5f, Time.deltaTime);
        }
        else
        {
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, ZoomOut, 5 * Time.deltaTime);
            vignetteFx.intensity.value = Mathf.MoveTowards(vignetteFx.intensity.value, 0.15f, Time.deltaTime);
            chromaticAberrationFx.intensity.value = Mathf.MoveTowards(chromaticAberrationFx.intensity.value, 0.07f, Time.deltaTime);
            lensDistortionFx.intensity.value = Mathf.MoveTowards(lensDistortionFx.intensity.value, 0, Time.deltaTime);
            bloomFx.intensity.value = Mathf.MoveTowards(bloomFx.intensity.value, 2.5f, Time.deltaTime);
        }
    }

    private void Aim(InputAction.CallbackContext ctx)
    {
        if(aimState != playerAimState.Aiming)
        {
            aimState = playerAimState.Aiming;
        }
    }

    private void StopAiming(InputAction.CallbackContext ctx)
    {
        if(aimState != playerAimState.NotAiming)
        {
            aimState = playerAimState.NotAiming;
        }
    }

    private void SwitchToPrimary(InputAction.CallbackContext ctx)
    {
        SecondaryWeapon.SetActive(false);
        PrimaryWeapon.SetActive(true);
        gunState = playerGunsState.Primary;
    }

    private void SwitchToSecondary(InputAction.CallbackContext ctx)
    {
        PrimaryWeapon.SetActive(false);
        SecondaryWeapon.SetActive(true);
        gunState = playerGunsState.Secondary;
    }

    private void SwitchWeapon(InputAction.CallbackContext ctx)
    {
        if(gunState == playerGunsState.Primary)
        {
            PrimaryWeapon.SetActive(false);
            SecondaryWeapon.SetActive(true);
            gunState = playerGunsState.Secondary;
        }

        if (gunState == playerGunsState.Secondary)
        {
            SecondaryWeapon.SetActive(false);
            PrimaryWeapon.SetActive(true);
            gunState = playerGunsState.Primary;
        }
    }
}
