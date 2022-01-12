using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetection : MonoBehaviour
{
    public Image detectionImage;
    public float detection;
    public GameObject detectedText;
    public List<CivilianRayCaster> detectedCivilians;

    private void Update()
    {
        detectionImage.fillAmount = detection / 100;

        if(detection == 100)
        {
            detectedText.SetActive(true);
        } else
            detectedText.SetActive(false);
    }
}
