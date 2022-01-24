using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Awake()
    {
        playerInput = new PlayerControls();
        playerInput.Enable();
    }

    private void Update()
    {
        playerInput.KeyboardInputs.Aim.performed += Aim;
        playerInput.KeyboardInputs.Aim.canceled += StopAiming;
        playerInput.KeyboardInputs.SwitchPrimaryWeapon.performed += SwitchToPrimary;
        playerInput.KeyboardInputs.SwitchSecondary.performed += SwitchToSecondary;
        playerInput.KeyboardInputs.MouseSwitchWeapon.performed += SwitchWeapon;
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
