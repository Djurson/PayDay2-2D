using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;

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
    }

    private void Start()
    {
        if(FirebaseManager.instance.user != null)
        {
            LoadProfile();
        }
    }

    private void LoadProfile()
    {
        if(FirebaseManager.instance.user != null)
        {
            string username;
            if (FirebaseManager.instance.user.DisplayName != null)
            {
                username = FirebaseManager.instance.user.DisplayName;
                usernameText.text = $"{beforeUsername} {username}";
            }
            else
            {
                username = $"Unknown Error";
                usernameText.text = username;
            }
        }
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
