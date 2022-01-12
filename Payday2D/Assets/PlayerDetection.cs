using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetection : MonoBehaviour
{
    public Image detectionImage;
    public float detection;

    private void Update()
    {
        detectionImage.fillAmount = detection / 100;
    }
}
