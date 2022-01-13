using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartupMenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject LoginMenu;

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            LoginMenu.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
