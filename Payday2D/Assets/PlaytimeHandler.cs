using System.Collections;
using UnityEngine;

public class PlaytimeHandler : MonoBehaviour
{
    public static PlaytimeHandler instance;

    [Header("Playtime")]
    public int playtime;
    public int minutes;
    public int hours;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine("playtimeCalculator");
    }

    private IEnumerator playtimeCalculator()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (true)
        {
            yield return delay;
            playtime += 1;

            minutes = (playtime / 60) % 60;
            hours = (playtime / 3600);
        }
    }
}
