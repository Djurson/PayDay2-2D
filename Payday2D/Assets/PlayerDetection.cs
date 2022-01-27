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
    public List<GuardRayCaster> detectedGuards;

    int i = 0;
    int x = 0;
    int h = 0;
    int j = 0;

    private void Update()
    {
        checkDetection();

        if (detectedCivilians.Count > 0 || detectedGuards.Count > 0)
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
        if (detectedCivilians.Count > 0 && detectedGuards.Count == 0)
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
        
        if (detectedGuards.Count > 0 && detectedCivilians.Count == 0)
        {
            detection = detectedGuards[0].localDetection;
            for (x = 0; x < detectedGuards.Count; x++)
            {
                if (detectedGuards[x].localDetection >= detection)
                {
                    detection = detectedGuards[x].localDetection;
                }
            }

            if (x == detectedGuards.Count)
                x = 0;
        }

        if (detectedCivilians.Count > 0 && detectedGuards.Count > 0)
        {
            detection = detectedCivilians[0].localDetection;
            for (h = 0; h < detectedCivilians.Count; h++)
            {
                if (detectedCivilians[h].localDetection >= detection)
                {
                    detection = detectedCivilians[h].localDetection;
                }
            }

            for (j = 0; j < detectedGuards.Count; j++)
            {
                if (detectedGuards[j].localDetection >= detection)
                {
                    detection = detectedGuards[j].localDetection;
                }
            }

            if (j == detectedGuards.Count)
                j = 0;

            if (h == detectedCivilians.Count)
                h = 0;
        }
        else
            detection = Mathf.MoveTowards(detection, 0, 5 * Time.deltaTime);
    }
}
