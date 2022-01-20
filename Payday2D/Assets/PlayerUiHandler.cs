using UnityEngine;
using Firebase.Auth;
using TMPro;

public class PlayerUiHandler : MonoBehaviour
{
    [Header("Firebase")]
    public FirebaseUser user;

    [Header("Text")]
    [SerializeField] private TMP_Text UsernameText;

    private void Start()
    {
        user = GameHandler.instance.user;
        UsernameText.text = $"{user.DisplayName}";
    }
}
