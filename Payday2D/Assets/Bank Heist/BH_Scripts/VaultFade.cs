using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultFade : MonoBehaviour
{
    public SpriteRenderer localSpriteRenderer;

    public Color color;

    public bool fadeIn;
    public bool fadeOut;

    private void Update()
    {
        if (fadeIn == true)
        {
            color.a = Mathf.MoveTowards(color.a, 255, Time.deltaTime);
        }

        if (fadeOut == true)
        {
            color.a = Mathf.MoveTowards(color.a, 0, Time.deltaTime);
        }

        localSpriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fadeIn = true;
            fadeOut = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fadeIn = false;
            fadeOut = true;
        }
    }
}
