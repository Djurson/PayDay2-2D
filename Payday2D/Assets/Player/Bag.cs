using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bag : MonoBehaviour
{
    public float interactionTime;

    private Rigidbody2D rb;

    public float lerpTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(rb != null)
        {
            rb.angularVelocity = 0;

            if (rb.velocity.x > 0 || rb.velocity.y > 0)
            {
                Vector2 wantedVelocity = Vector2.Lerp(rb.velocity, Vector2.zero, lerpTime * Time.deltaTime);

                rb.velocity = wantedVelocity;
            }

            if (rb.velocity.x < 0 || rb.velocity.y < 0)
            {
                Vector2 wantedVelocity = Vector2.Lerp(rb.velocity, Vector2.zero, lerpTime * Time.deltaTime);

                rb.velocity = wantedVelocity;
            }
        }
    }
}
