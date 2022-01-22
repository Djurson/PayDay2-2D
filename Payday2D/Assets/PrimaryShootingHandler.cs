using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public enum WeaponFireMode
{
    Semi,
    Auto
};

public class PrimaryShootingHandler : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform RootTransform;
    [SerializeField] private Transform Player;
    [Space(5)]

    [Header("Enums")]
    public WeaponFireMode localFireMode;

    [Header("Recoil")]
    [SerializeField] private Vector2 PrimaryRecoilNotAiming;
    [SerializeField] private Vector2 PrimaryRecoilAiming;

    [Header("Ammo")]
    public int primaryTotalAmmo; 
    public int primaryMagAmmo;
    [SerializeField] private int primaryMaxMagAmmo;
    [SerializeField] private bool isReloading = false;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform BulletStart;
    [Space(5)]

    [Header("Animation Vectors")]
    [SerializeField] private Vector2 AdsPosition;
    [SerializeField] private Vector2 NormalPosition;

    [Header("Fire Time")]
    [SerializeField] private float PrimaryFireRate;
    private float primaryLastTimeFired;

    [Header("Inputs")]
    [SerializeField] private float mouseFireInput;
    [SerializeField] private float reloadInput;
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
        this.transform.localPosition = NormalPosition;
        localFireMode = WeaponFireMode.Auto;
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
        reloadInput = playerInput.KeyboardInputs.Reload.ReadValue<float>();
        playerInput.KeyboardInputs.ChangeFireMode.performed += ChangeFireMode;
        playerInput.KeyboardInputs.MouseFire.performed += SemiAutomaticFire;

        if(reloadInput == 1)
        {
            StartCoroutine("Reload");
        }
    }

    private void ChangeFireMode(InputAction.CallbackContext ctx)
    {
        if (localFireMode == WeaponFireMode.Auto)
            localFireMode = WeaponFireMode.Semi;

        else localFireMode = WeaponFireMode.Auto;
    }

    private void ADSAnimations()
    {
        if (gunHandler.aimState == playerAimState.Aiming)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, AdsPosition, 8 * Time.deltaTime);
        }

        if(gunHandler.aimState == playerAimState.NotAiming)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, NormalPosition, 8 * Time.deltaTime);
        }
    }

    private void Shooting()
    {
        if(primaryMagAmmo == 0 || isReloading) { return; }

        if (mouseFireInput == 1 && primaryMagAmmo > 0 && gunHandler.aimState == playerAimState.NotAiming && localFireMode == WeaponFireMode.Auto)
        {
            if (Time.time - primaryLastTimeFired > 1 / PrimaryFireRate)
            {
                primaryLastTimeFired = Time.time;
                RootTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(PrimaryRecoilNotAiming.x, PrimaryRecoilNotAiming.y));
                primaryMagAmmo -= 1;
                var instatiatedBullet = Instantiate(BulletPrefab, BulletStart.position, Quaternion.identity);
                instatiatedBullet.gameObject.GetComponent<Rigidbody2D>().velocity = Player.transform.up * 20;
            }
        }

        if (mouseFireInput == 1 && primaryMagAmmo > 0 && gunHandler.aimState == playerAimState.Aiming && localFireMode == WeaponFireMode.Auto)
        {
            if (Time.time - primaryLastTimeFired > 1 / PrimaryFireRate)
            {
                primaryLastTimeFired = Time.time;
                RootTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(PrimaryRecoilAiming.x, PrimaryRecoilAiming.y));
                primaryMagAmmo -= 1;
                var instatiatedBullet = Instantiate(BulletPrefab, BulletStart.position, Quaternion.identity);
                instatiatedBullet.gameObject.GetComponent<Rigidbody2D>().velocity = Player.transform.up * 20;
            }
        }
    }

    private void SemiAutomaticFire(InputAction.CallbackContext ctx)
    {
        if (primaryMagAmmo == 0 || isReloading) { return; }

        if (primaryMagAmmo > 0 && gunHandler.aimState == playerAimState.NotAiming && localFireMode == WeaponFireMode.Semi)
        {
            if (Time.time - primaryLastTimeFired > 1 / PrimaryFireRate)
            {
                primaryLastTimeFired = Time.time;
                RootTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(PrimaryRecoilAiming.x, PrimaryRecoilAiming.y));
                primaryMagAmmo -= 1;
                var instatiatedBullet = Instantiate(BulletPrefab, BulletStart.position, Quaternion.identity);
                instatiatedBullet.gameObject.GetComponent<Rigidbody2D>().velocity = Player.transform.up * 20;
            }
        }

        if (mouseFireInput == 1 && primaryMagAmmo > 0 && gunHandler.aimState == playerAimState.Aiming && localFireMode == WeaponFireMode.Semi)
        {
            if (Time.time - primaryLastTimeFired > 1 / PrimaryFireRate)
            {
                primaryLastTimeFired = Time.time;
                RootTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(PrimaryRecoilAiming.x, PrimaryRecoilAiming.y));
                primaryMagAmmo -= 1;
                var instatiatedBullet = Instantiate(BulletPrefab, BulletStart.position, Quaternion.identity);
                instatiatedBullet.gameObject.GetComponent<Rigidbody2D>().velocity = Player.transform.up * 20;
            }
        }
    }

    private IEnumerator Reload()
    {
        if (primaryMagAmmo != primaryMaxMagAmmo && primaryTotalAmmo > 0)
        {
            isReloading = true;
            int UsedAmmo = primaryMaxMagAmmo - primaryMagAmmo;

            yield return new WaitForSeconds(2);

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
            isReloading = false;
            StopCoroutine("Reload");
        }
    }
}
