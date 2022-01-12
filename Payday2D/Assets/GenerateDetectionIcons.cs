using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDetectionIcons : MonoBehaviour
{
    private GameObject[] civilians;

    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private GameObject detectionIcon;

    private void Start()
    {
        civilians = GameObject.FindGameObjectsWithTag("Civilian");

        for(int i = 0; i < civilians.Length; i++)
        {
            var instatiatedIcon = Instantiate(detectionIcon, parentTransform);
            civilians[i].GetComponent<CivilianRayCaster>().detectionIcon = instatiatedIcon.gameObject;
            instatiatedIcon.gameObject.GetComponent<PlayerPositionToScreenPoint>().player = civilians[i];
        }
    }
}
