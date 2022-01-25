using UnityEngine;
using TMPro;
using Firebase.Auth;
using System.Collections;
using Firebase;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;

    [Header("Firebase")]
    public FirebaseUser user;
    public FirebaseAuth auth;
    [Space(5f)]

    //[Header("UI References")]
    //[SerializeField] private GameObject profileUI;
    //[SerializeField] private GameObject changeEmailUI;
    //[SerializeField] private GameObject changePasswordUI;
    //[SerializeField] private GameObject reverifyUI;
    //[SerializeField] private GameObject resetPasswordConfirmUI;
    //[SerializeField] private GameObject deleteUserConfirmUI;
    //[Space(5f)]

    [Header("Basic Info References")]
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private string beforeUsername;
    private string email;

    //[Header("Change Email References")]
    //[SerializeField]
    //private TMP_InputField changeEmailEmailInputField;
    //[Space(5f)]

    //[Header("Change Password References")]
    //[SerializeField]
    //private TMP_InputField changePasswordInputField;
    //[SerializeField]
    //private TMP_InputField changePasswordConfirmInputField;
    //[Space(5f)]

    //[Header("Reverify References")]
    //[SerializeField]
    //private TMP_InputField reverifyEmailInputField;
    //[SerializeField]
    //private TMP_InputField reverifyPasswordInputField;
    //[Space(5)]

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(gameObject);
        }
        if (FirebaseManager.instance.user != null)
        {
            user = FirebaseManager.instance.user;
            auth = FirebaseManager.instance.auth;
        }
        if(GameHandler.instance != null)
        {
            user = GameHandler.instance.user;
            auth = GameHandler.instance.auth;
        }
    }

    private void Start()
    {
        LoadProfile();
    }

    private void LoadProfile()
    {
        usernameText.text = (user != null ? usernameText.text = $"{beforeUsername} {user.DisplayName}" : usernameText.text = $"Unknown Error");
    }


    public void UserSignOut()
    {
        auth.SignOut();
    }
    //public void CleanUI()
    //{
    //    profileUI.SetActive(false);
    //}

    //public void ProfileUI()
    //{
    //    CleanUI();
    //    profileUI.SetActive(true);
    //    LoadProfile();
    //}
}
