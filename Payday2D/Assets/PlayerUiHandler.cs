using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUiHandler : MonoBehaviour
{
    [Header("Objects")]
    public GameObject playingUi;
    public GameObject FailedHeistUi;

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
        playingUi.SetActive(true);
        FailedHeistUi.SetActive(false);

        if(FirebaseManager.instance != null && FirebaseManager.instance.user != null)
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
        if(PlayerInteraction.bodyBags != 0 && PlayerInteraction.cableTies != 0)
        {
            SpecialText.text = $"{PlayerInteraction.bodyBags}/{PlayerInteraction.cableTies}";
        }
        else if(PlayerInteraction.bodyBags == 0 && PlayerInteraction.cableTies != 0)
        {
            SpecialText.text = $"<color=#FF3333>{PlayerInteraction.bodyBags}</color=#FF3333>/{PlayerInteraction.cableTies}";
        } else if(PlayerInteraction.bodyBags != 0 && PlayerInteraction.cableTies == 0)
        {
            SpecialText.text = $"{PlayerInteraction.bodyBags}/<color=#FF3333>{PlayerInteraction.cableTies}</color=#FF3333>";
        }
        else
        {
            SpecialText.text = $"<color=#FF3333>{PlayerInteraction.bodyBags}/{PlayerInteraction.cableTies}</color=#FF3333>";
        }

        if(GameManager.instance.heistState == HeistState.Failed)
        {
            playingUi.SetActive(false);
            FailedHeistUi.SetActive(true);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        PlaytimeHandler.instance.SendData();
        GameManager.instance.ClearData();
        FirebaseManager.instance.UpdateUserData(LevelHandler.instance.levelSystem.level, LevelHandler.instance.levelSystem.experience, GameManager.instance.PlayerCurrentSpendableCash, GameManager.instance.PlayerCurrentOffshoreAccount, GameManager.instance.HeistsCompleted, GameManager.instance.PlayTimeInHeistsSeconds + PlaytimeHandler.instance.playtime);
        SceneManager.LoadSceneAsync(levelIndex);
    }
}
