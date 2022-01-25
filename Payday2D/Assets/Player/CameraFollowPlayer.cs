using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private GameObject cam;
    public GameObject cameraFade;
    public float lerpTime;

    private void Update()
    {
        cam = GameObject.Find("Main Camera");
    }

    private void FixedUpdate()
    {
        if (cam != null)
        {
            Vector3 camPos = cam.transform.position;

            Vector3 desiredPos = transform.position;

            Vector3 smoothedPos = Vector3.Lerp(camPos, desiredPos, lerpTime);

            cam.transform.position = new Vector3(smoothedPos.x, smoothedPos.y, -1);
        }
    }
}
