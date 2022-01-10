using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class civilianRandomizer : MonoBehaviour
{
    public SpriteRenderer HairSr;
    public SpriteRenderer BaseSr;

    public List<Color> HairColors;
    public List<Color> BaseColors;

    private bool fadeIn;
    private bool fadeOut;

    public Color HairChoosen;
    public Color BaseChoosen;

    void Start()
    {
        HairChoosen = HairColors[Random.Range(0, HairColors.Count)];
        BaseChoosen = BaseColors[Random.Range(0, BaseColors.Count)];

        BaseSr.color = BaseChoosen;
        HairSr.color = HairChoosen;
    }

    private void Update()
    {
        var HairColor = HairSr.color;
        var BaseColor = BaseSr.color;

        if (fadeIn == true)
        {
            HairSr.enabled = true;
            BaseSr.enabled = true;

            HairColor.a = Mathf.MoveTowards(HairColor.a, 255, Time.deltaTime);
            BaseColor.a = Mathf.MoveTowards(BaseColor.a, 255, Time.deltaTime);
        }

        if (fadeOut == true)
        {
            HairColor.a = Mathf.MoveTowards(HairColor.a, 0, Time.deltaTime);
            BaseColor.a = Mathf.MoveTowards(BaseColor.a, 0, Time.deltaTime);
        }

        if(fadeOut == true && HairColor.a == 0 && BaseColor.a == 0)
        {
            HairSr.enabled = false;
            BaseSr.enabled = false;
        }

        HairSr.color = HairColor;
        BaseSr.color = BaseColor;
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
