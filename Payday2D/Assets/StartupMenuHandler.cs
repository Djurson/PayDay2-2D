using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartupMenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject savingIcon;

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            AuthUiManager.instance.LoginScreen();
            this.gameObject.SetActive(false);
            savingIcon.SetActive(false);
        }
    }
}
