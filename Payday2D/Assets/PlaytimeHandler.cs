using System.Collections;
using UnityEngine;

public class PlaytimeHandler : MonoBehaviour
{
    public static PlaytimeHandler instance;

    [Header("Playtime")]
    public int playtime;
    public int seconds;
    public int minutes;
    public int hours;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    public void StopCounting()
    {
        StopCoroutine("AddHeistPlaytime");
    }

    private void Start()
    {
        StartCoroutine("AddHeistPlaytime");
    }

    public void SendData()
    {
        StopCounting();
        GameManager.instance.PlayTimeInHeistsSeconds += playtime;
    }

    private IEnumerator AddHeistPlaytime()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (true)
        {
            yield return delay;
            playtime += 1;
            seconds = (playtime % 60);
            minutes = (playtime / 60) % 60;
            hours = (playtime / 3600);
        }
    }
}