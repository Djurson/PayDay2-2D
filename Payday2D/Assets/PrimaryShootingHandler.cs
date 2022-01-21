using UnityEngine;
using UnityEngine.InputSystem;

public class PrimaryShootingHandler : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform RootTransform;
    [Space(5)]

    [Header("Recoil")]
    [SerializeField] private Vector2 PrimaryRecoilNotAiming;

    [Header("Ammo")]
    [SerializeField] private int primaryTotalAmmo;
    [SerializeField] private int primaryMagAmmo;
    [SerializeField] private int primaryMaxMagAmmo;
    [Space(5)]

    [Header("Fire Time")]
    [SerializeField] private float PrimaryFireRate;
    private float primaryLastTimeFired;

    [Header("Animators")]
    [SerializeField] private Animator animator;

    [Header("Inputs")]
    [SerializeField] private float mouseFireInput;
    private PlayerControls playerInput;

    [Header("Scripts")]
    [SerializeField] private PlayerGunHandler gunHandler;

    private void Awake()
    {
        playerInput = new PlayerControls();
        playerInput.Enable();
    }

    private void Start()
    {
        gunHandler = GetComponentInParent<PlayerGunHandler>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        PlayerInput();
        ADSAnimations();
        Shooting();
    }

    private void PlayerInput()
    {
        mouseFireInput = playerInput.KeyboardInputs.MouseFire.ReadValue<float>();
        playerInput.KeyboardInputs.Reload.performed += Reload;
    }

    private void ADSAnimations()
    {
        if(gunHandler.aimState == playerAimState.Aiming)
            animator.SetBool("isAiming", true);

        if(gunHandler.aimState == playerAimState.NotAiming)
            animator.SetBool("isAiming", false);
    }

    private void Shooting()
    {
        if (mouseFireInput == 1 && primaryMagAmmo > 0 && gunHandler.aimState == playerAimState.NotAiming)
        {
            if (Time.time - primaryLastTimeFired > 1 / PrimaryFireRate)
            {
                primaryLastTimeFired = Time.time;
                RootTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(PrimaryRecoilNotAiming.x, PrimaryRecoilNotAiming.y));
                primaryMagAmmo -= 1;
            }
        }
    }

    private void Reload(InputAction.CallbackContext context)
    {
        if(primaryMagAmmo != primaryMaxMagAmmo && primaryTotalAmmo > 0)
        {
            int UsedAmmo = primaryMaxMagAmmo - primaryMagAmmo;

            if (primaryTotalAmmo > UsedAmmo)
            {
                primaryTotalAmmo -= UsedAmmo;
                primaryMagAmmo = primaryMaxMagAmmo;
            }
            else
            {
                primaryMagAmmo = primaryTotalAmmo;
                primaryTotalAmmo = 0;
            }
        }
    }
}
