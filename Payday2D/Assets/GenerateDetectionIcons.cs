using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDetectionIcons : MonoBehaviour
{
    private GameObject[] civilians;
    private GameObject[] guards;

    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private GameObject detectionIcon;

    private void Start()
    {
        civilians = GameObject.FindGameObjectsWithTag("Civilian");
        guards = GameObject.FindGameObjectsWithTag("Guard");

        for(int i = 0; i < civilians.Length; i++)
        {
            var instatiatedIcon = Instantiate(detectionIcon, parentTransform);
            civilians[i].GetComponent<CivilianRayCaster>().detectionIcon = instatiatedIcon.gameObject;
            instatiatedIcon.gameObject.GetComponent<PlayerPositionToScreenPoint>().player = civilians[i];
        }

        for(int j = 0; j < guards.Length; j++)
        {
            var instatiatedIcon = Instantiate(detectionIcon, parentTransform);
            guards[j].GetComponent<GuardRayCaster>().detectionIcon = instatiatedIcon.gameObject;
            instatiatedIcon.gameObject.GetComponent<PlayerPositionToScreenPoint>().player = guards[j];
        }
    }
}
