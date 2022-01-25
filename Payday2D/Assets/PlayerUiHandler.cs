using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUiHandler : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite AutomaticIcon;
    [SerializeField] private Sprite SemiAutomaticIcon;

    [Header("Images")]
    [SerializeField] private Image PrimaryFireModeImage;
    [SerializeField] private Image SecondaryFireModeImage;

    [Header("Text")]
    [SerializeField] private TMP_Text UsernameText;
    [SerializeField] private TMP_Text PrimaryAmmoText;
    [SerializeField] private TMP_Text SecondaryAmmoText;
    [SerializeField] private TMP_Text SpecialText;

    [Header("Script Refrences")]
    [SerializeField] private PlayerGunHandler gunHandler;
    [SerializeField] private WeaponShootingHandler PrimaryWeapon;
    [SerializeField] private WeaponShootingHandler SecondaryWeapon;
    [SerializeField] private playerInteraction PlayerInteraction;

    private void Start()
    {
        if(FirebaseManager.instance.user != null)
        {
            UsernameText.text = $"{FirebaseManager.instance.user.DisplayName}";
        }
    }

    private void Update()
    {
        if(gunHandler.gunState == playerGunsState.Primary)
        {
            PrimaryFireModeImage.color = Color.Lerp(SecondaryFireModeImage.color, new Color(255f, 255f, 255f, 255f), Time.deltaTime);
            SecondaryFireModeImage.color = Color.Lerp(SecondaryFireModeImage.color, new Color(255f, 255f, 255f, 125f), Time.deltaTime);

            if (PrimaryWeapon.localFireMode == WeaponFireMode.Auto)
            {
                PrimaryFireModeImage.sprite = AutomaticIcon;
            }
            else if (PrimaryWeapon.localFireMode == WeaponFireMode.Semi)
            {
                PrimaryFireModeImage.sprite = SemiAutomaticIcon;
            }
        }

        if (gunHandler.gunState == playerGunsState.Secondary)
        {
            SecondaryFireModeImage.color = Color.Lerp(SecondaryFireModeImage.color, new Color(255f, 255f, 255f, 255f), Time.deltaTime);
            PrimaryFireModeImage.color = Color.Lerp(PrimaryFireModeImage.color, new Color(255f, 255f, 255f, 125f), Time.deltaTime);

            if (SecondaryWeapon.localFireMode == WeaponFireMode.Auto)
            {
                SecondaryFireModeImage.sprite = AutomaticIcon;
            }
            else if (SecondaryWeapon.localFireMode == WeaponFireMode.Semi)
            {
                SecondaryFireModeImage.sprite = SemiAutomaticIcon;
            }
        }

        SecondaryAmmoText.text = $"{SecondaryWeapon.MagAmmo}/{SecondaryWeapon.TotalAmmo}";
        PrimaryAmmoText.text = $"{PrimaryWeapon.MagAmmo}/{PrimaryWeapon.TotalAmmo}";
        if(PlayerInteraction.bodyBags != 0)
        {
            SpecialText.text = $"{PlayerInteraction.bodyBags}";
        }
        else
        {
            SpecialText.text = $"<color=#FF3333>{PlayerInteraction.bodyBags}</color=#FF3333>";
        }
    }
}
