using System.Collections;
using UnityEngine;

public class BulletHoleFadeOut : MonoBehaviour
{
    private bool fade = false;

    [SerializeField] private SpriteRenderer sprieRenderer;

    private void Start()
    {
        StartCoroutine("FadeBulletHole");
    }

    private void Update()
    {
        if (fade)
        {
            sprieRenderer.color = Color.Lerp(sprieRenderer.color, new Color(0f, 0f, 0f, 0f), Time.deltaTime);
        }

        if(sprieRenderer.color.a <= 0.02f)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FadeBulletHole()
    {
        yield return new WaitForSeconds(5f);
        fade = true;
        StopCoroutine("FadeBulletHole");
    }
}
