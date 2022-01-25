using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApplicationVersion : MonoBehaviour
{
    [SerializeField] private TMP_Text versionText;

    void Start()
    {
        versionText.text = $"Version: {Application.version}";
    }
}
