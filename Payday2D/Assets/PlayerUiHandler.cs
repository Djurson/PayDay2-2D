using UnityEngine;
using Firebase.Auth;
using TMPro;
using UnityEngine.UI;

public class PlayerUiHandler : MonoBehaviour
{
    [Header("Firebase")]
    public FirebaseUser user;

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

    [Header("Script Refrences")]
    [SerializeField] private PrimaryShootingHandler PrimaryWeapon;

    private void Start()
    {
        if(GameHandler.instance.user != null)
        {
            user = GameHandler.instance.user;
            UsernameText.text = $"{user.DisplayName}";
        }
    }

    private void Update()
    {
        if(PrimaryWeapon.localFireMode == WeaponFireMode.Auto)
        {
            PrimaryFireModeImage.sprite = AutomaticIcon;
        } else if(PrimaryWeapon.localFireMode == WeaponFireMode.Semi)
        {
            PrimaryFireModeImage.sprite = SemiAutomaticIcon;
        }

        PrimaryAmmoText.text = $"{PrimaryWeapon.primaryMagAmmo}/{PrimaryWeapon.primaryTotalAmmo}";
    }
}
