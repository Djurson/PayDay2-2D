using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionToScreenPoint : MonoBehaviour
{
    private Camera cam;
    public GameObject player;
    public Vector2 offset;
    public float lerpTime;

    private void Start()
    {
        cam = Camera.main;
        Vector2 textDesiredPos = cam.WorldToScreenPoint(player.transform.position);
        transform.position = new Vector2(offset.x + textDesiredPos.x, offset.y + textDesiredPos.y);
    }

    private void FixedUpdate()
    {
        Vector2 textDesiredPos = cam.WorldToScreenPoint(player.transform.position);
        transform.position = Vector2.Lerp(transform.position, new Vector2(offset.x + textDesiredPos.x, offset.y + textDesiredPos.y), lerpTime);
    }
}
