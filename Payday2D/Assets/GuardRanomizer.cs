using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRanomizer : MonoBehaviour
{
    public SpriteRenderer BaseSr;
    public SpriteRenderer HairSr;
    public SpriteRenderer GunSr;

    public List<Color> BaseColors;

    private bool fadeIn;
    private bool fadeOut;

    public Color BaseChoosen;

    void Start()
    {
        BaseChoosen = BaseColors[Random.Range(0, BaseColors.Count)];

        BaseSr.color = BaseChoosen;
    }

    private void Update()
    {
        var BaseColor = BaseSr.color;
        var HairColor = HairSr.color;
        var GunColor = GunSr.color;

        if (fadeIn == true)
        {
            HairSr.enabled = true;
            BaseSr.enabled = true;
            GunSr.enabled = true;

            HairColor.a = Mathf.MoveTowards(HairColor.a, 255, Time.deltaTime);
            BaseColor.a = Mathf.MoveTowards(BaseColor.a, 255, Time.deltaTime);
            GunColor.a = Mathf.MoveTowards(BaseColor.a, 255, Time.deltaTime);
        }

        if (fadeOut == true)
        {
            HairColor.a = Mathf.MoveTowards(HairColor.a, 0, Time.deltaTime);
            BaseColor.a = Mathf.MoveTowards(BaseColor.a, 0, Time.deltaTime);
            GunColor.a = Mathf.MoveTowards(BaseColor.a, 255, Time.deltaTime);
        }

        if (fadeOut == true && HairColor.a == 0 && BaseColor.a == 0)
        {
            HairSr.enabled = false;
            BaseSr.enabled = false;
            GunSr.enabled = false;
        }

        HairSr.color = HairColor;
        BaseSr.color = BaseColor;
        GunSr.color = GunColor;
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
