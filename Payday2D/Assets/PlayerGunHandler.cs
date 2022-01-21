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

    private void Awake()
    {
        playerInput = new PlayerControls();
        playerInput.Enable();
    }

    private void Update()
    {
        playerInput.KeyboardInputs.Aim.performed += Aim;
        playerInput.KeyboardInputs.Aim.canceled += StopAiming;
    }

    private void Aim(InputAction.CallbackContext ctx)
    {
        if(aimState != playerAimState.Aiming)
        {
            Debug.Log(ctx.ReadValue<float>());
            aimState = playerAimState.Aiming;
        }
    }

    private void StopAiming(InputAction.CallbackContext ctx)
    {
        if(aimState != playerAimState.NotAiming)
        {
            Debug.Log(ctx.ReadValue<float>());
            aimState = playerAimState.NotAiming;
        }
    }
}
