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

    int i = 0;

    private void Update()
    {
        checkDetection();

        if (detectedCivilians.Count > 0)
        {
            detectionImage.fillAmount = detection / 100;

            if (detection == 100)
            {
                detectedText.SetActive(true);
            }
            else
                detectedText.SetActive(false);
        }
        else
        {
            detectionImage.fillAmount = 0;
            detectedText.SetActive(false);
        }
    }

    private void checkDetection()
    {
        if (detectedCivilians.Count > 0)
        {
            detection = detectedCivilians[0].localDetection;
            for (i = 0; i < detectedCivilians.Count; i++)
            {
                if (detectedCivilians[i].localDetection >= detection)
                {
                    detection = detectedCivilians[i].localDetection;
                }
            }

            if (i == detectedCivilians.Count)
                i = 0;
        }
        else 
            detection = 0;
    }
}
